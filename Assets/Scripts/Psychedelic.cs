using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Psychedelic : MonoBehaviour
{
    [SerializeField]
    private float delta = 0.1f;
    private float hue;
    private Renderer rend;
    private Renderer[] renderers;
    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        float H, S, V;
        Color.RGBToHSV(rend.material.color, out H, out S, out V);
        hue = H + Random.Range(0, 360);

        renderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        hue = Mathf.Repeat(hue + delta, 360);
        
        Color c = Color.HSVToRGB(Mathf.Round(hue) / 360, 1, 1);

        foreach(Renderer r in renderers) {
            r.material.color = c;
        }
    }
}
