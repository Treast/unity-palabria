using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeColor : MonoBehaviour
{
    [SerializeField]
    bool shouldHideMaterial = false;

    void Awake()
    {
        if(shouldHideMaterial) {
            Renderer[] renderers = GetComponentsInChildren<Renderer>();

            foreach(Renderer r in renderers) {
                Color color = r.material.color;
                r.material.color = new Color(color.r, color.g, color.b, 0);

                r.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                r.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                r.material.SetInt("_ZWrite", 0);
                r.material.DisableKeyword("_ALPHATEST_ON");
                r.material.EnableKeyword("_ALPHABLEND_ON");
                r.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                r.material.renderQueue = 3002;
            }
        }
    }

    public void ShowMaterial(float duration) {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer r in renderers)
        {
            Color color = r.material.color;
            r.material.DOColor(new Color(color.r, color.g, color.b, 1), duration);
        }
    }
}
