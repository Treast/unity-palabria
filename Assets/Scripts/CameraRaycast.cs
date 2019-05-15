using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    public Material hoverMaterial;
    public Material normalMaterial;

    private GameObject collidedGameObject;

    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit))
        {
            if (hit.collider.CompareTag("Interactive"))
            {
                collidedGameObject = hit.collider.gameObject;
                setHover();
            }
        }
        else
        {
            setOut();
        }
    }

    private void setHover()
    {
        MeshRenderer meshRenderer = collidedGameObject.GetComponent<MeshRenderer>();
        if (meshRenderer) meshRenderer.material = hoverMaterial;
    }

    private void setOut()
    {
        if (collidedGameObject)
        {
            MeshRenderer meshRenderer = collidedGameObject.GetComponent<MeshRenderer>();
            if (meshRenderer) meshRenderer.material = normalMaterial;
            collidedGameObject = null;
        }
    }
}
