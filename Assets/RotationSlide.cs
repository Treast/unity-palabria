using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationSlide : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider slidePosition;
    public Slider slideRotation;
    public bool rotation = false;

    void Start()
    {
        EventManager.StartListening("rotation", rotationActive);
    }

    void rotationActive()
    {
        Debug.Log("J'active la rotation");
        rotation = true;
    }

    void RotationSubmit() {
        rotation = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(rotation) {
            Vector3 rot = transform.rotation.eulerAngles;
            Vector3 v = new Vector3(0, slideRotation.value, 0);
            GameObject.FindGameObjectWithTag("Exp7").transform.localRotation = Quaternion.Euler(v);
            GameObject.FindGameObjectWithTag("Exp7").transform.rotation = Quaternion.Euler(v);
        }
    }
}
