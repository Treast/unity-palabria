using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AudioSelector : MonoBehaviour
{
    [SerializeField]
    Image audioImageLoader;
    AudioSource audioSource;
    AudioClip audioClip;
    Animator animator;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioClip = audioSource.clip;
        GameObject audioSelector = GameObject.FindGameObjectsWithTag("AudioSelector")[0];
        animator = audioSelector.GetComponent<Animator>();
    }

    void Update()
    {
        if(audioSource.isPlaying) {
            audioImageLoader.fillAmount = audioSource.time / audioClip.length;
        }

        float progress = Mathf.Clamp01(audioSource.time / audioClip.length);
        if(progress == 1.0f) {
            audioImageLoader.DOFillAmount(0, 1.0f); 
            animator.SetTrigger("OffAnimation");
        }
    }
}
