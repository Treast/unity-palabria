using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerAnimator : MonoBehaviour
{
    [SerializeField] string eventToListen = "";
    [SerializeField] string triggerName = "";
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        EventManager.StartListening(eventToListen, Trigger);
    }

    void Trigger() {
        animator.SetTrigger(triggerName);
    }
}
