using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class GenerateImageAnchorManager : MonoBehaviour
{


    [SerializeField]
    private ARReferenceImage referenceImage;

    [SerializeField]
    private GameObject prefabToGenerate;

    private GameObject imageAnchorGO;

    private bool planeDetect = false;

    public int cpt = 0;
    public int endLoop = 60;

    // Use this for initialization
    void Start()
    {
        UnityARSessionNativeInterface.ARImageAnchorAddedEvent += AddImageAnchor;
        UnityARSessionNativeInterface.ARImageAnchorUpdatedEvent += UpdateImageAnchor;
        //UnityARSessionNativeInterface.ARImageAnchorRemovedEvent += RemoveImageAnchor;

        EventManager.StartListening("PlaneDetect", PlaneDetect);

    }

    void PlaneDetect() {
        planeDetect = true;

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
                Debug.Log("Je place");
                imageAnchorGO.transform.position = UnityARMatrixOps.GetPosition(arImageAnchor.transform);
                imageAnchorGO.transform.eulerAngles = Vector3.up * UnityARMatrixOps.GetRotation(arImageAnchor.transform).eulerAngles.y;

            }
            else if (imageAnchorGO.activeSelf)
            {
                imageAnchorGO.SetActive(false);
            }

            cpt++;
        }

    }

    /*void RemoveImageAnchor(ARImageAnchor arImageAnchor)
    {
        Debug.LogFormat("image anchor removed[{0}] : tracked => {1}", arImageAnchor.identifier, arImageAnchor.isTracked);
        if (imageAnchorGO)
        {
            GameObject.Destroy(imageAnchorGO);
        }

    }*/

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
