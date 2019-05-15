using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Selector : MonoBehaviour
{
    GameObject cross;
    Animator crossAnimator;

    // Use this for initialization
    void Awake()
    {

        GameObject parent = gameObject.transform.parent.gameObject;
        foreach (Transform child in parent.transform)
        {
            if (child.gameObject.tag == "SelectorCross")
            {
                cross = child.gameObject;
                break;
            }
        }


        crossAnimator = cross.GetComponent<Animator>();

    }

    void OnSelectionAnimationFinished()
    {
        Debug.Log("TRIGGERERERE");
        EventManager.TriggerEvent("OnSelectionAnimationFinished");
    }

    public void showInteractionPointer()
    {

        Debug.Log("[interaction] showing pointer");
        crossAnimator.ResetTrigger("Cross_Disappear");
        crossAnimator.SetTrigger("Cross_Appear");
    }

    public void hideInteractionPointer()
    {
        Debug.Log("[interaction] hiding pointer");
        crossAnimator.ResetTrigger("Cross_Appear");
        crossAnimator.SetTrigger("Cross_Disappear");
    }


    // Update is called once per frame
    void Update()
    {

    }
}