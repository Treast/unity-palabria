using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CameraRaycastAnimation : MonoBehaviour
{
    public PlayableDirector director;

    private GameObject collidedGameObject;

    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit))
        {
            if (hit.collider.CompareTag("Interactive") && collidedGameObject == null)
            {
                collidedGameObject = hit.collider.gameObject;
                director.Play();
            }
        }
    }
}
