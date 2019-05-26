using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector3 : MonoBehaviour
{
    GameObject pointer;
    GameObject lastGameObjectHit = null;
    Animator pointerAnimator;
    Image pointerLoader;
    bool isReady = false;



    [SerializeField] float selectionDuration = 3.0f;
    float currentTime = 0.0f;
    bool hasTriggered = false;

    // Use this for initialization
    void Awake()
    {
        GameObject[] pointers = GameObject.FindGameObjectsWithTag("Pointer");
        foreach (GameObject p in pointers)
        {
            pointer = p;
        }

        pointerAnimator = pointer.GetComponent<Animator>();
        pointerLoader = GameObject.FindGameObjectWithTag("PointerLoader").GetComponent<Image>();
        EventManager.StartListening("SceneIsReady", SceneIsReady);
    }

    void SceneIsReady() {
        isReady = true;
    }

    void LateUpdate()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit))
        {
            Selectable3 selectable = hit.collider.GetComponent<Selectable3>();
            if (selectable)
            {
                if (lastGameObjectHit != hit.collider.gameObject)
                {

                    lastGameObjectHit = hit.collider.gameObject;
                    selectable.TriggerPointerEnter();
                    showInteractionPointer();
                } else {
                    if(!hasTriggered) {

                       
                        currentTime += Time.deltaTime;
                        float p = Mathf.Clamp01(currentTime / selectionDuration);
                        pointerLoader.color = new Color(1, 1, 1, 1);
                        pointerLoader.fillAmount = p;
                        pointerAnimator.SetBool("IsSelecting", true);

                        if (p >= 1)
                        {
                            Debug.Log("hasTriggered 2");
                            hasTriggered = true;
                            pointerAnimator.SetBool("IsSelecting", false);
                            EventManager.TriggerEvent("AudioGonnaPlay");
                            pointerAnimator.SetTrigger("PlayAnimation");
                            selectable.TriggerPointerCompleted();
                            currentTime = 0;
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("DEBUG");
            currentTime = 0;
            pointerAnimator.SetBool("IsSelecting", false);
            hasTriggered = false;
            if (lastGameObjectHit)
            {
                Selectable2 selectable = lastGameObjectHit.GetComponent<Selectable2>();
                if (selectable)
                {
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
