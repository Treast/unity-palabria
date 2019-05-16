using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameObjectPosition : MonoBehaviour
{
    [SerializeField] bool ignoreRotation = false;
    [SerializeField] Vector3 offsetVector = new Vector3(0, 0, 0);

    public void SetPosition(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        if (!ignoreRotation)
        {
            transform.rotation = rotation;
        }
        transform.Translate(offsetVector, Space.World);
    }

    public void SetPositionToOrigin()
    {
        transform.position = new Vector3(0, 0, 0);
    }
}
