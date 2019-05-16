using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameObjectPosition : MonoBehaviour
{
    [SerializeField] bool ignoreRotation = false;

    public void SetPosition(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        if(!ignoreRotation) {
            transform.rotation = rotation;
        }
    }

    public void SetPositionToOrigin() {
        transform.position = new Vector3(0, 0, 0);
    }
}
