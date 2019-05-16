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
    UnityARSessionNativeInterface m_session;
    public UnityARPlaneDetection planeDetection;

    // Use this for initialization
    void Start()
    {
        EventManager.StartListening("PlaneDetect", PlaneDetect);
        m_session = UnityARSessionNativeInterface.GetARSessionNativeInterface();

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

                if (Mathf.Clamp01(cpt / endLoop) == 1) {

                    // Placement de l'objet
                    // Piste
                    // https://gist.github.com/otmb/28621781f88dc6a34b35e1edb2740fb2

                    imageAnchorGO.transform.position = UnityARMatrixOps.GetPosition(arImageAnchor.transform);
                    imageAnchorGO.transform.eulerAngles = Vector3.up * UnityARMatrixOps.GetRotation(arImageAnchor.transform).eulerAngles.y;

                    Destroy(imageLoader);
                    //Camera.main.WorldToViewportPoint(imageAnchorGO.transform.position);
                    //Destroy(GameObject.FindGameObjectWithTag("generatePlane"));
                    GameObject.FindGameObjectWithTag("StartingScreen").GetComponent<Animator>().SetTrigger("OffAnimation");
                }
            }
            else if (imageAnchorGO.activeSelf)
            {
                imageAnchorGO.SetActive(false);
            }

        }

    }

    IEnumerator RemoveAnchorCoroutine(ARImageAnchor anchor)
    {
        yield return new WaitForSeconds(3.0f);
        //doesn't have to be in a coroutine, but for my uses it is
        m_session.RemoveUserAnchor(anchor.identifier);
    }

    public void planeDetectionOFF()
    {
        planeDetection = UnityARPlaneDetection.None;
        ARKitWorldTrackingSessionConfiguration config = new ARKitWorldTrackingSessionConfiguration();
        config.planeDetection = planeDetection;
        //config.alignment = startAlignment;
        config.getPointCloudData = false;
        config.enableLightEstimation = false;
        m_session.RunWithConfig(config);
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
}
