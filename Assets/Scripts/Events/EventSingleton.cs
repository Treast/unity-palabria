using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSingleton : MonoBehaviour
{
    public void TriggerEvent(string eventName) {
        EventManager.TriggerEvent(eventName);
    }
}
