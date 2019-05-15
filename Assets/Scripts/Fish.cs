using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.02f;

    private float noise = 0.0f;

    [SerializeField]
    private float noiseFactor = 0.03f;
    void Start()
    {
        noise = Random.Range(0.0f, 10.0f);
    }

    void Update()
    {
        float currentNoiseX = Mathf.PerlinNoise(-noise * noiseFactor, -noise * noiseFactor);
        float currentNoiseY = Mathf.PerlinNoise(noise * noiseFactor, noise * noiseFactor);
        float currentNoiseZ = Mathf.PerlinNoise(-noise * noiseFactor, noise * noiseFactor);

        float angleX = (currentNoiseX - 0.5f) * Mathf.Rad2Deg * Mathf.PI * 2;
        float angleY = (currentNoiseY - 0.5f) * Mathf.Rad2Deg * Mathf.PI * 2;
        float angleZ = (currentNoiseZ - 0.5f) * Mathf.Rad2Deg * Mathf.PI * 2;

        transform.rotation = Quaternion.Euler(angleX, angleY, angleZ);

        transform.position += transform.forward * speed;

        noise = noise + 1 * noiseFactor;
    }
}
