using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("scanImage", pointerToScanImage);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void startAnimationOutro()
    {
        EventManager.TriggerEvent("endStartAnimationOutro");
    }

    void startAnimationEnd()
    {
        EventManager.TriggerEvent("endStartAnimationEnd");
    }


    void startAnimationToScanImage()
    {
        EventManager.TriggerEvent("endStartAnimationOutro");
        Animator scanImage = GameObject.FindGameObjectWithTag("AudioSelector").GetComponent<Animator>();
    }

    void endAnimationPointerToScanImage()
    {
        EventManager.TriggerEvent("PlaneDetect");
    }

    void pointerToScanImage()
    {
        GameObject.FindGameObjectWithTag("StartingScreen").GetComponent<Animator>().SetTrigger("ScanImage");
    }
}