using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.UI;
using System;

public class DebugPlacement : MonoBehaviour
{


    [SerializeField]
    private ARReferenceImage referenceImage;

    private GameObject imageAnchorGO;

    // Use this for initialization
    void Start()
    {
        UnityARSessionNativeInterface.ARImageAnchorAddedEvent += AddImageAnchor;
    }

    public void DebugClick() {
    }

    void AddImageAnchor(ARImageAnchor arImageAnchor)
    {
        if (arImageAnchor.referenceImageName == referenceImage.imageName)
        {
            Vector3 position = Vector3.up * (UnityARMatrixOps.GetPosition(arImageAnchor.transform).y - 1.7f);
            GameObject.FindGameObjectWithTag("Exp7").transform.position = position;

            ///GameObject.FindGameObjectWithTag("Exp7").transform.rotation = GameObject.FindGameObjectWithTag("Exp7").transform.position

            //GameObject.FindGameObjectWithTag("StartingScreen").GetComponent<Animator>().SetTrigger("OffAnimation");
        }
    }
    void OnDestroy()
    {
        UnityARSessionNativeInterface.ARImageAnchorAddedEvent -= AddImageAnchor;
    }
}
