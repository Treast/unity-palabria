using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class SetWorldOriginFromImageSet : MonoBehaviour
{
    [SerializeField]
    private ARReferenceImage referenceImage = null;

    private GameObject imageAnchorGO;
    // Start is called before the first frame update
    void Start()
    {
        UnityARSessionNativeInterface.ARImageAnchorAddedEvent += AddImageAnchor;

    }
    void AddImageAnchor(ARImageAnchor arImageAnchor)
    {
        Debug.LogFormat("image anchor added[{0}] : tracked => {1}", arImageAnchor.identifier, arImageAnchor.isTracked);
        if (arImageAnchor.referenceImageName == referenceImage.imageName)
        {
            Vector3 position = UnityARMatrixOps.GetPosition(arImageAnchor.transform);
            Quaternion rotation = UnityARMatrixOps.GetRotation(arImageAnchor.transform);

            GameObject go = new GameObject();
            go.transform.position = position;
            go.transform.rotation = Quaternion.FromToRotation(Vector3.up, position);

            UnityARSessionNativeInterface.GetARSessionNativeInterface().SetWorldOrigin(go.transform);
        }
    }

    void OnDestroy()
    {
        UnityARSessionNativeInterface.ARImageAnchorAddedEvent -= AddImageAnchor;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
