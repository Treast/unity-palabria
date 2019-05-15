using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;

public class SetMaterialInChildren : MonoBehaviour
{
    [SerializeField]
    private Material material = null;

    void Start()
    {
        SetMaterial();
    }

    public void SetMaterial() {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            renderer.material = material;
            var materials = new Material[renderer.materials.Length];

            for (int i = 0; i < renderer.materials.Length; i++)
            {
                materials[i] = material;
            }

            renderer.materials = materials;
        }
        // System.Guid guid = System.Guid.NewGuid();
        // material.DOColor(new Color(70, 62, 0), "_EmissionColor", 5).SetLoops(-1, LoopType.Yoyo).SetId(guid);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SetMaterialInChildren))]
public class SetMaterialInChildrenEditor: Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SetMaterialInChildren setMaterial = (SetMaterialInChildren)target;
        if(GUILayout.Button("Set material")) {
            setMaterial.SetMaterial();
        }
    }
}
#endif