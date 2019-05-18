using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAudio : MonoBehaviour
{
    AudioSource audioSource;
    Animator pointerAnimator;
    Image pointerLoader;
    bool hasStarted = false;
    bool hasTriggered = false;

    void Start() {
        pointerAnimator = GameObject.FindGameObjectsWithTag("Pointer")[0].GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        pointerLoader = GameObject.FindGameObjectsWithTag("PointerLoader")[0].GetComponent<Image>();
    }

    public void Play() {
        StartCoroutine(PlayCoRoutine());
    }

    IEnumerator PlayCoRoutine() {
        yield return new WaitForSeconds(2.2f);
        pointerLoader.fillAmount = 0;
        audioSource.Play();
        hasTriggered = false;
        hasStarted = true;
    }

    void LateUpdate()
    {
        if(audioSource.isPlaying) {
            float p = Mathf.Clamp01(audioSource.time / audioSource.clip.length);
            pointerLoader.color = new Color(0, 1, 1, 1);
            pointerLoader.fillAmount = p;
        } else {
            if (hasStarted && !hasTriggered)
            {
                hasTriggered = true;
                pointerAnimator.SetTrigger("StopAnimation");
            }
        }        
    }
}
