using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EmissiveMaterial : MonoBehaviour
{
    Material fakeMaterial;
    Material material;
    Renderer[] renderers;

    void Start()
    {
        fakeMaterial = new Material(Shader.Find("Standard"));
        fakeMaterial.color = new Color(0, 0, 0, 1);
        material = GetComponentsInChildren<Renderer>()[0].material;
        renderers = GetComponentsInChildren<Renderer>();

        fakeMaterial.DOColor(new Color(0.6f, 0, 0, 1), 2).SetLoops(-1, LoopType.Yoyo);
    }

    void Update() {
        
        foreach(Renderer rend in renderers) {
            rend.material.SetColor("_EmissionColor", fakeMaterial.color);
        }
    }
}
