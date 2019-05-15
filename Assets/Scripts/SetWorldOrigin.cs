using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class SetWorldOrigin : MonoBehaviour
{
    // Start is called before the first frame update
    public void SetOrigin(Vector3 position, Quaternion rotation)
    {
        Transform t = new GameObject().transform;
        t.position = position;
        t.rotation = rotation;

        UnityARSessionNativeInterface.GetARSessionNativeInterface().SetWorldOrigin(t);
    }
}
