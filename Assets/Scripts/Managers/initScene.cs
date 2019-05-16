using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.UI;

public class initScene : MonoBehaviour
{


    [SerializeField]
    private ARReferenceImage referenceImage;

    [SerializeField]
    private GameObject prefabToGenerate;

    private GameObject imageAnchorGO;
  
	public Image imageLoader; 
	private bool planeDetect = false;

    public float cpt = 0;
    public float endLoop = 60;

    public bool firstInit = false;

    public Vector3 moyennePosition;
    public Vector3 moyenneRotation;
    // Use this for initialization
    void Start()
    {
        EventManager.StartListening("PlaneDetect", PlaneDetect);
    }
    

    void PlaneDetect()
    {
        planeDetect = true;
        if(!firstInit) {
            Debug.Log("Premier");
            UnityARSessionNativeInterface.ARImageAnchorAddedEvent += AddImageAnchor;
            UnityARSessionNativeInterface.ARImageAnchorUpdatedEvent += UpdateImageAnchor;
            firstInit = true;
        }
    }


    void AddImageAnchor(ARImageAnchor arImageAnchor)
    {
        Debug.LogFormat("image anchor added[{0}] : tracked => {1}", arImageAnchor.identifier, arImageAnchor.isTracked);
        if (arImageAnchor.referenceImageName == referenceImage.imageName)
        {
            Vector3 position = UnityARMatrixOps.GetPosition(arImageAnchor.transform);
            Quaternion rotation = UnityARMatrixOps.GetRotation(arImageAnchor.transform);

            imageAnchorGO = Instantiate<GameObject>(prefabToGenerate, position, rotation);

        }
    }

    void UpdateImageAnchor(ARImageAnchor arImageAnchor)
    {
        Debug.LogFormat("image anchor updated[{0}] : tracked => {1}", arImageAnchor.identifier, arImageAnchor.isTracked);
        if (arImageAnchor.referenceImageName == referenceImage.imageName && planeDetect && cpt < endLoop)
        {
            if (arImageAnchor.isTracked)
            {
                if (!imageAnchorGO.activeSelf)
                {
                    imageAnchorGO.SetActive(true);
                    
                }
                cpt++;
                imageLoader.fillAmount = Mathf.Clamp01(cpt / endLoop);
                moyennePosition += UnityARMatrixOps.GetPosition(arImageAnchor.transform);
                moyenneRotation += Vector3.up * UnityARMatrixOps.GetRotation(arImageAnchor.transform).eulerAngles.y;

                if (Mathf.Clamp01(cpt / endLoop) == 1)
                
                    // Placement de l'objet
                    imageAnchorGO.transform.position = moyennePosition / endLoop;
                    imageAnchorGO.transform.eulerAngles = moyenneRotation / endLoop;
                    Destroy(imageLoader);
                    Destroy(GameObject.FindGameObjectWithTag("generatePlane"));
                    GameObject.FindGameObjectWithTag("StartingScreen").GetComponent<Animator>().SetTrigger("OffAnimation");
                }
            }
            else if (imageAnchorGO.activeSelf)
            {
                imageAnchorGO.SetActive(false);
            }

        }


    }


    private void LateUpdate()
    {
    }



    void OnDestroy()
    {
        UnityARSessionNativeInterface.ARImageAnchorAddedEvent -= AddImageAnchor;
        UnityARSessionNativeInterface.ARImageAnchorUpdatedEvent -= UpdateImageAnchor;
        //UnityARSessionNativeInterface.ARImageAnchorRemovedEvent -= RemoveImageAnchor;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
