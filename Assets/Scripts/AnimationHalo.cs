using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationHalo : MonoBehaviour
{
    [SerializeField] float minHalo = 0.0f;
    [SerializeField] float maxHalo = 0.5f;
    [SerializeField] float speed = 2.0f;

    Light light;

    void Start()
    {
        light = GetComponent<Light>();
        light.DOIntensity(maxHalo, speed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
}
