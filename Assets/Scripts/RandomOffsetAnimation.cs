using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomOffsetAnimation : MonoBehaviour
{
    void Start()
    {
        GetComponent<Animator>().SetFloat("Offset", Random.Range(0.0f, 1.0f));   
    }
}
