using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayAudio : MonoBehaviour
{
    AudioSource audioSource;
    Animator pointerAnimator;
    Image pointerLoader;
    bool hasStarted = false;
    bool hasTriggered = false;
    bool shouldDisplayLoader = true;

    void Start() {
        pointerAnimator = GameObject.FindGameObjectsWithTag("Pointer")[0].GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        pointerLoader = GameObject.FindGameObjectsWithTag("PointerLoader")[0].GetComponent<Image>();
        EventManager.StartListening("AudioStart", FadeOut);
        EventManager.StartListening("AudioGonnaPlay", GonnaPlay);
    }

    void FadeOut() {
        audioSource.DOFade(0, 1.8f);
        StartCoroutine(Stop());
    }

    void GonnaPlay() {
        shouldDisplayLoader = false;
        pointerLoader.fillAmount = 1;
    }

    IEnumerator Stop() {
        yield return new WaitForSeconds(1.8f);
        hasTriggered = true;
        audioSource.Stop();
        pointerAnimator.SetBool("MusicIsPlaying", false);
    }

    public void Play() {
        EventManager.TriggerEvent("AudioStart");
        StartCoroutine(PlayCoRoutine());
    }

    IEnumerator PlayCoRoutine() {
        yield return new WaitForSeconds(2.2f);
        pointerLoader.fillAmount = 0;
        audioSource.volume = 1;
        shouldDisplayLoader = true;
        audioSource.Play();
        hasTriggered = false;
        hasStarted = true;
        pointerAnimator.SetBool("MusicIsPlaying", true);
    }

    void LateUpdate()
    {
        if(audioSource.isPlaying) {
            if(shouldDisplayLoader) {
                float p = Mathf.Clamp01(audioSource.time / audioSource.clip.length);
                pointerLoader.color = new Color(0, 1, 1, 1);
                pointerLoader.fillAmount = p;
            }
        } else {
            if (hasStarted && !hasTriggered)
            {
                hasTriggered = true;
                pointerAnimator.SetBool("MusicIsPlaying", false);
                pointerAnimator.SetTrigger("StopAnimation");
            }
        }        
    }
}
