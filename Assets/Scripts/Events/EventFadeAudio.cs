using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EventFadeAudio : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        EventManager.StartListening("FadeAudio", FadeAudio);
    }

    void FadeAudio()
    {
        if(audioSource.isPlaying) {
            audioSource.DOFade(0, 2);
        }
    }

    public void FadeAllAudio() {
        EventManager.TriggerEvent("FadeAudio");
    }
}
