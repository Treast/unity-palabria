using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSubtitle : MonoBehaviour
{
    SubtitleDisplayer displayer;
    [SerializeField] TextAsset subtitle;

    public void Start() {
        GameObject go = GameObject.FindGameObjectsWithTag("Subtitles")[0];
        displayer = go.GetComponent<SubtitleDisplayer>();
    }

    public void SetSubtitle() {
        displayer.Stop();
        displayer.Subtitle = subtitle;
        StartCoroutine(PlaySubtitle());
    }

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
