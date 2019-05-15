using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePrefab : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    void Start()
    {
    }

    void Update()
    {
        if(Random.value < 0.01) {
            float x = Random.Range(-0.5f, 0.5f);
            float z = Random.Range(-0.5f, 0.5f);

            GameObject go = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
            go.transform.parent = transform;
            go.transform.localPosition = new Vector3(x, 0.0f, z);
            go.AddComponent<Medusa>();
        }
    }
}
