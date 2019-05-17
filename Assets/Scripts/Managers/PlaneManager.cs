using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace UnityEngine.XR.iOS
{
    public class PlaneManager : MonoBehaviour
    {
        public GameObject planePrefab;
        private UnityARAnchorManager unityARAnchorManager;

        public bool iniPlane = false;

        // Use this for initialization
        void Start () {

            unityARAnchorManager = new UnityARAnchorManager();
            UnityARUtility.InitializePlanePrefab(planePrefab);
            Debug.Log("Init Plane Manager");
            EventManager.StartListening("endStartAnimationOutro", initPlaneManager);

        }

        private void initPlaneManager()
        {
            Debug.Log("Generate Plane True");
            iniPlane = true;
        }

        void OnDestroy()
        {
            unityARAnchorManager.Destroy ();
        }

        void OnGUI()
        {

        }

        private void Update()
        {

            if(iniPlane) {
                IEnumerable<ARPlaneAnchorGameObject> arpags = unityARAnchorManager.GetCurrentPlaneAnchors();
                foreach (var planeAnchor in arpags)
                {
                    EventManager.TriggerEvent("PlaneDetect");
                }
            }
        }
    }
}

