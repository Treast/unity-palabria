using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyingGameObject : MonoBehaviour
{
    // [SerializeField]
    // private float noiseFactor = 0.05f;
    // [SerializeField]
    // private float noiseDelta = 1.0f;

    [SerializeField]
    private float maxFloatingDistance = 0.5f;
    [SerializeField]
    private float speed = 5.0f;
    // private float noise = 0.0f;
    // private float originY = 0.0f;
    void Start()
    {
        // originY = transform.position.y;
        System.Guid guid = System.Guid.NewGuid();
        transform.position = new Vector3(transform.position.x, transform.position.y - maxFloatingDistance / 2, transform.position.z);
        Tween tween = transform.DOMoveY(transform.position.y + maxFloatingDistance, speed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetId(guid);
        DOTween.Goto(guid, Random.Range(0, speed), true);
    }

    void Update()
    {
        // float noiseY = Mathf.PerlinNoise(transform.position.x + noise * noiseFactor, transform.position.z + noise * noiseFactor) * 2 - 1;
        // transform.position = new Vector3(transform.position.x, originY + noiseY * maxFloatingDistance, transform.position.z);
        // noise += noiseDelta;
    }
}
