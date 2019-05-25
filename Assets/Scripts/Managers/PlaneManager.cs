using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace UnityEngine.XR.iOS
{
    public class PlaneManager : MonoBehaviour
    {
        public GameObject planePrefab;
        private UnityARAnchorManager unityARAnchorManager;

        public bool initPlane = false;


        // Use this for initialization
        void Start()
        {

            unityARAnchorManager = new UnityARAnchorManager();
            //UnityARUtility.InitializePlanePrefab(planePrefab);
            EventManager.StartListening("endStartAnimationOutro", initPlaneManager);


        }

        private void initPlaneManager()
        {
            Animator endStartAnimationOutro = GameObject.FindGameObjectWithTag("Sprite").GetComponent<Animator>();
            endStartAnimationOutro.SetTrigger("FadeOn");
            initPlane = true;
        }

        void OnDestroy()
        {
            unityARAnchorManager.Destroy();
        }

        void OnGUI()
        {

        }

        private void Update()
        {

            if (initPlane)
            {
                IEnumerable<ARPlaneAnchorGameObject> arpags = unityARAnchorManager.GetCurrentPlaneAnchors();

                foreach (var planeAnchor in arpags)
                {

                    if (unityARAnchorManager.GetCurrentPlaneAnchors().Count >= 1)
                    {
                        EventManager.TriggerEvent("scanImage");
                        initPlane = false;
                    }
                }
            }
        }
    }
}

