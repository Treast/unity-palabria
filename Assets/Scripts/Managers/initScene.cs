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
        UnityARSessionNativeInterface.ARImageAnchorAddedEvent += AddImageAnchor;
        UnityARSessionNativeInterface.ARImageAnchorUpdatedEvent += UpdateImageAnchor;
    }


    void PlaneDetect()
    {
        planeDetect = true;
        if (!firstInit)
        {
            Debug.Log("Premier");

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
            rotation.w = 0;
            rotation.x = 0;
            rotation.z = 0;
            position.y = position.y + 10000;

            imageAnchorGO = Instantiate<GameObject>(prefabToGenerate, position, rotation);
            firstInit = true;
            WaitScanImage();
        }
    }

    void UpdateImageAnchor(ARImageAnchor arImageAnchor)
    {

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

                    if (Mathf.Clamp01(cpt / endLoop) == 1)
                    {

                        // Placement de l'objet
                        // Piste
                        // https://gist.github.com/otmb/28621781f88dc6a34b35e1edb2740fb2

                        imageAnchorGO.transform.position = UnityARMatrixOps.GetPosition(arImageAnchor.transform);
                        imageAnchorGO.transform.eulerAngles = Vector3.up * (UnityARMatrixOps.GetRotation(arImageAnchor.transform).eulerAngles.y + 90.0f);
                        //StopImageTracking();
                        UnityARSessionNativeInterface.ARImageAnchorUpdatedEvent -= UpdateImageAnchor;
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

    void StopImageTracking()
    {
        Debug.Log("GameManager: StopImageDetection");

        // Update session
        UnityARSessionNativeInterface m_session = UnityARSessionNativeInterface.GetARSessionNativeInterface();
        ARKitWorldTrackingSessionConfiguration config = new ARKitWorldTrackingSessionConfiguration();
        config.planeDetection = UnityARPlaneDetection.None;
        config.referenceImagesGroupName = null;
        config.maximumNumberOfTrackedImages = 0;
        config.getPointCloudData = false;
        config.alignment = UnityARAlignment.UnityARAlignmentGravity;
        config.enableLightEstimation = true;
        config.enableAutoFocus = true;
        m_session.RunWithConfig(config);

        // We destroy all GeneratorAnchor script to prevent created anchor to update
        //Destroy(GameBoardGenerateImageAnchor);
        // TODO the same thing but for PanelControl
        // Destroy (PanelControlGenerateImageAnchor);

    }


    private void LateUpdate()
    {
    }


    private void Update()
    {
        Debug.Log(GameObject.FindGameObjectWithTag("PointerLoader").GetComponent<Image>());
        //Debug.Log(imageAnchorGO.transform.position);
    }

    IEnumerator WaitScanImage()
    {
        yield return new WaitForSeconds(1);
        UnityARSessionNativeInterface.ARImageAnchorUpdatedEvent += UpdateImageAnchor;
    }
    void OnDestroy()
    {
        UnityARSessionNativeInterface.ARImageAnchorAddedEvent -= AddImageAnchor;

        //UnityARSessionNativeInterface.ARImageAnchorRemovedEvent -= RemoveImageAnchor;

    }
}
