using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selectable3 : MonoBehaviour
{
    bool hasTriggered = false;
    public EventTrigger.TriggerEvent OnPointerEnter;
    public EventTrigger.TriggerEvent OnPointerEnterOnce;
    public EventTrigger.TriggerEvent OnPointerSelected;
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

    public void TriggerPointerCompleted() {
        OnPointerSelected.Invoke(null);
        Debug.Log("Trigger Pointer Selected");
    }
}
