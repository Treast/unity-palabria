using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FleurEmissive : MonoBehaviour
{
    void Start()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            renderer.material.DOColor(new Color(70, 62, 0), "_EmissionColor", 5).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
