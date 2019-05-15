using UnityEngine;
using TMPro;
using UnityEditor;

public class Subtitle : MonoBehaviour
{
    private float timeLapse = 0.4f;

    [SerializeField]
    private TextMeshPro textMeshPro = null;

    private AudioSource audioSource = null;

    [SerializeField]
    private float timeBegin = 0.0f;

    [SerializeField]
    private float timeEnd = 0.0f;

    [SerializeField]
    private string text = "";

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if(audioSource.isPlaying) {
            if(audioSource.time >= timeBegin && audioSource.time < timeEnd) {
                bool isOnEdge = false;

                if (audioSource.time <= timeBegin + timeLapse)
                {
                    isOnEdge = true;
                    float t = (audioSource.time - timeBegin) / timeLapse;
                    float alpha = Lerp(0, 1, t);
                    textMeshPro.color = new Color(255, 255, 255, alpha);
                }

                if (audioSource.time >= timeEnd - timeLapse)
                {
                    isOnEdge = true;
                    float t = (audioSource.time - timeEnd + timeLapse) / timeLapse;
                    float alpha = Lerp(1, 0, t);
                    textMeshPro.color = new Color(255, 255, 255, alpha);
                }

                if(!isOnEdge) {
                    textMeshPro.color = new Color(255, 255, 255, 1);
                }


                textMeshPro.text = text;
            }
        }
    }

    float Lerp(float a, float b, float n) {
        return (1 - n) * a + n * b;
    }

    public void PlayAudio() {
        audioSource.Play();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Subtitle))]
public class SubtitleEditor: Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Subtitle subtitle = (Subtitle)target;
        if(GUILayout.Button("Play Audio")) {
            subtitle.PlayAudio();
        }
    }
}
#endif