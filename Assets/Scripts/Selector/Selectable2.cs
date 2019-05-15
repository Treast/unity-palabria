using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selectable2 : MonoBehaviour
{
    bool hasTriggered = false;
    public EventTrigger.TriggerEvent OnPointerEnter;
    public EventTrigger.TriggerEvent OnPointerEnterOnce;
    public EventTrigger.TriggerEvent OnPointerLeave;

    public void TriggerPointerEnter()
    {
        OnPointerEnter.Invoke(null);
        Debug.Log("Trigger Pointer Enter");

        if(!hasTriggered) {
            OnPointerEnterOnce.Invoke(null);
            hasTriggered = true;
            Debug.Log("Trigger Pointer EnterOnce");
        }
    }
    public void TriggerPointerLeave()
    {
        OnPointerLeave.Invoke(null);
    }
}
