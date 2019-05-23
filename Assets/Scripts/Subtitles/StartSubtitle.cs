using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSubtitle : MonoBehaviour
{
    public void Run()
    {
        // Typically Begin would be called from the same place that starts the video
        StartCoroutine(PlaySubtitle());
    }

    IEnumerator PlaySubtitle() {
        yield return new WaitForSeconds(1.8f);
        StartCoroutine(FindObjectOfType<SubtitleDisplayer>().Begin());
    }
}
