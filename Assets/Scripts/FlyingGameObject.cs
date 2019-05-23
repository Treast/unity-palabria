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

    // [SerializeField]
    // private float maxFloatingXDistance = 0.3f;
    [SerializeField]
    private float speed = 5.0f;
    // private float noise = 0.0f;
    // private float originY = 0.0f;
    void Start()
    {
        // originY = transform.position.x;
        System.Guid guid = System.Guid.NewGuid();
        System.Guid guid2 = System.Guid.NewGuid();
        transform.position = new Vector3(transform.position.x, transform.position.y - maxFloatingDistance / 2, transform.position.z);
        Tween tween = transform.DOMoveY(transform.position.y + maxFloatingDistance, speed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetId(guid);
        Tween tween2 = transform.DOMoveX(transform.position.x + maxFloatingDistance / 2, speed * 2).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetId(guid2);
        DOTween.Goto(guid, Random.Range(0, speed), true);
        DOTween.Goto(guid2, Random.Range(0, speed * 2), true);
        transform.DORotate(new Vector3(0, 360, 0), 120, RotateMode.LocalAxisAdd);
    }

    void Update()
    {
        // float noiseY = Mathf.PerlinNoise(noise * noiseFactor, transform.position.z + noise * noiseFactor) * 2 - 1;
        // transform.position = new Vector3(transform.position.x + noiseY * maxFloatingXDistance, transform.position.y, transform.position.z);
        // noise += noiseDelta;
    }
}
