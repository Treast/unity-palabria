using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObscurable : MonoBehaviour
{
    void Start()
    {
        // get all renderers in this object and its children:
        var renders = GetComponentsInChildren<Renderer>();
        foreach (Renderer rendr in renders)
        {
            rendr.material.renderQueue = 3002; // set their renderQueue
        }
    }
}
