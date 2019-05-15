using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToTurnAround = null;

    [SerializeField]
    private float minimumRadius = 1;

    [SerializeField]
    private float maximumRadius = 1;

    [SerializeField]
    private float minimumY = 1;

    [SerializeField]
    private float maximumY = 1;

    private float saveY = 0;

    private float angle;

    private float speed = 20;

    private float noise = 0;
    private float noiseDelta = 0.04f;
    private float noiseFactor = 0.1f;

    void Start()
    {
        Vector2 originVector = new Vector2(objectToTurnAround.transform.position.x, objectToTurnAround.transform.position.z);
        Vector2 objectVector = new Vector2(transform.position.x, transform.position.z);
        angle = Vector2.Angle(originVector, objectVector);
        saveY = transform.position.y;
    }

    void Update()
    {
        angle += 1f / speed;

        float n = Mathf.PerlinNoise(objectToTurnAround.transform.position.x + noise * noiseFactor, objectToTurnAround.transform.position.y + noise * noiseFactor);

        float mp = n * 2 - 1;

        float y = saveY + minimumY + mp * (maximumY - minimumY);
        float x = (minimumRadius + n * (maximumRadius - minimumRadius)) * Mathf.Cos(angle) + objectToTurnAround.transform.position.x;
        float z = (minimumRadius + n * (maximumRadius - minimumRadius)) * Mathf.Sin(angle) + objectToTurnAround.transform.position.z;

        Debug.Log((minimumRadius + n * (maximumRadius - minimumRadius)));

        transform.position = new Vector3(x, y, z);

        noise += noiseDelta;
    }
}
