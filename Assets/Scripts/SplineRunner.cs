using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class SplineRunner : MonoBehaviour
{
    [SerializeField]
    private float duration = 10.0f;
    [SerializeField]
    private GameObject movingObject = null;
    private Spline spline;
    void Start()
    {
        spline = GetComponent<Spline>();
        Tween.Spline(spline, movingObject.transform, 0, 1, true, duration, 0, Tween.EaseLinear, Tween.LoopType.Loop);
    }

    void Update()
    {
        
    }
}
