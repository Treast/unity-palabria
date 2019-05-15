using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Selector2 : MonoBehaviour
{
    GameObject pointer;
    GameObject lastGameObjectHit = null;
    Animator pointerAnimator;

    private bool isTriggerOn = false;
    private bool isTriggerOff = false;

    // Use this for initialization
    void Awake()
    {
        GameObject[] pointers = GameObject.FindGameObjectsWithTag("Pointer");
        foreach (GameObject p in pointers)
        {
            pointer = p;
        }


        pointerAnimator = pointer.GetComponent<Animator>();
    }

    void Update() {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit))
        {
            Selectable2 selectable = hit.collider.GetComponent<Selectable2>();
            if (selectable)
            {
                if(lastGameObjectHit != hit.collider.gameObject) {
                    lastGameObjectHit = hit.collider.gameObject;
                    selectable.TriggerPointerEnter();
                    showInteractionPointer();
                }
            }
        } else {
            if(lastGameObjectHit) {
                Selectable2 selectable = lastGameObjectHit.GetComponent<Selectable2>();
                if(selectable) {
                    selectable.TriggerPointerLeave();
                }
                lastGameObjectHit = null;
            }
            hideInteractionPointer();
        }
    }

    void OnSelectionAnimationFinished()
    {
        EventManager.TriggerEvent("OnSelectionAnimationFinished");
    }

    public void showInteractionPointer()
    {
        pointerAnimator.ResetTrigger("OffAnimation");
        pointerAnimator.SetTrigger("OnAnimation");
    }

    public void hideInteractionPointer()
    {
        pointerAnimator.ResetTrigger("OnAnimation");
        pointerAnimator.SetTrigger("OffAnimation");
    }
}