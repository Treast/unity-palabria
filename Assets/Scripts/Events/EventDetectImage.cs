using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.iOS;

[System.Serializable]
public class ImageDetectionEvent : UnityEvent<Vector3, Quaternion>
{
}

public class EventDetectImage : MonoBehaviour
{
    [SerializeField]
    private ARReferenceImage referenceImage = null;

    [SerializeField]
    private ImageDetectionEvent callback = null;

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

            callback.Invoke(position, rotation);
        }
    }
}
