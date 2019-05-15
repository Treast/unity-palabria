using UnityEngine;

public class ShowGameObjectInEditor : MonoBehaviour
{
    void Awake()
    {
        if(!Application.isEditor) {
            transform.position = new Vector3(1000, 1000, 1000);
        } 
    }
}
