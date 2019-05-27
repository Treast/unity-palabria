using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EmissiveMaterial : MonoBehaviour
{
    Material fakeMaterial;
    Material material;
    Renderer[] renderers;

    [SerializeField] Color color;
    [SerializeField] float speed = 2.0f;

    void Start()
    {
        fakeMaterial = new Material(Shader.Find("Standard"));
        material = GetComponentsInChildren<Renderer>()[0].material;
        renderers = GetComponentsInChildren<Renderer>();
        fakeMaterial.color = material.color;

        fakeMaterial.DOColor(color, speed).SetLoops(-1, LoopType.Yoyo);
    }

    void Update() {
        
        foreach(Renderer rend in renderers) {
            rend.material.SetColor("_EmissionColor", fakeMaterial.color);
        }
    }
}

