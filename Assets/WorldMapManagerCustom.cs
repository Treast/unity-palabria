using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.UI; // Required when Using UI elements.

public class WorldMapManagerCustom : MonoBehaviour
{
    public GameObject cube;
    public GameObject passerelleGame;
    [SerializeField]
    UnityARCameraManager m_ARCameraManager;
    ARWorldMap m_LoadedMap;
    public Slider mainSlider;

    serializableARWorldMap serializedWorldMap;

    // Use this for initialization
    void Start()
    {
        UnityARSessionNativeInterface.ARFrameUpdatedEvent += OnFrameUpdate;
    }


    ARTrackingStateReason m_LastReason;

    void OnFrameUpdate(UnityARCamera arCamera)
    {
        if (arCamera.trackingReason != m_LastReason)
        {
            Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
            Debug.LogFormat("worldTransform: {0}", arCamera.worldTransform.column3);
            Debug.LogFormat("trackingState: {0} {1}", arCamera.trackingState, arCamera.trackingReason);
            m_LastReason = arCamera.trackingReason;
        }
    }

    public void AddedUserAnchor(ARUserAnchor anchor)
    {


        passerelleGame.transform.position = UnityARMatrixOps.GetPosition(anchor.transform);
        passerelleGame.transform.rotation = UnityARMatrixOps.GetRotation(anchor.transform);
        Debug.Log("AddedUser " + cube.transform.rotation);
    }

    static UnityARSessionNativeInterface session
    {
        get { return UnityARSessionNativeInterface.GetARSessionNativeInterface(); }
    }

    static string path
    {
        get { return Path.Combine(Application.persistentDataPath, "myFirstWorldMap.worldmap"); }
    }

    void OnWorldMap(ARWorldMap worldMap)
    {
        if (worldMap != null)
        {
            worldMap.Save(path);
            Debug.LogFormat("ARWorldMap saved to {0}", path);
        }
    }

    public void Save()
    {

        passerelleGame.transform.rotation = cube.transform.rotation;
        passerelleGame.transform.position = cube.transform.position;

        UnityARSessionNativeInterface.GetARSessionNativeInterface().AddUserAnchorFromGameObject(cube);
        session.GetCurrentWorldMapAsync(OnWorldMap);
    }

    public void Load()
    {
        Debug.LogFormat("Loading ARWorldMap {0}", path);
        var worldMap = ARWorldMap.Load(path);
        if (worldMap != null)
        {
            m_LoadedMap = worldMap;
            Debug.LogFormat("Map loaded. Center: {0} Extent: {1}", worldMap.center, worldMap.extent);

            UnityARSessionNativeInterface.ARSessionShouldAttemptRelocalization = true;
            //UnityARSessionNativeInterface.ARUserAnchorAdded = true;

            var config = m_ARCameraManager.sessionConfiguration;
            config.worldMap = worldMap;
            UnityARSessionRunOption runOption = UnityARSessionRunOption.ARSessionRunOptionRemoveExistingAnchors | UnityARSessionRunOption.ARSessionRunOptionResetTracking;

            Debug.Log("Restarting session with worldMap");
            session.RunWithConfigAndOptions(config, runOption);

            UnityARSessionNativeInterface.ARUserAnchorAddedEvent += AddedUserAnchor;

        }
    }


    void OnWorldMapSerialized(ARWorldMap worldMap)
    {
        if (worldMap != null)
        {
            //we have an operator that converts a ARWorldMap to a serializableARWorldMap
            serializedWorldMap = worldMap;
            Debug.Log("ARWorldMap serialized to serializableARWorldMap");
        }
    }

    public void submitActive()
    {
        EventManager.TriggerEvent("rotation");
    }


    public void SaveSerialized()
    {
        session.GetCurrentWorldMapAsync(OnWorldMapSerialized);
    }

    public void LoadSerialized()
    {
        Debug.Log("Loading ARWorldMap from serialized data");
        //we have an operator that converts a serializableARWorldMap to a ARWorldMap
        ARWorldMap worldMap = serializedWorldMap;
        if (worldMap != null)
        {
            m_LoadedMap = worldMap;
            Debug.LogFormat("Map loaded. Center: {0} Extent: {1}", worldMap.center, worldMap.extent);

            UnityARSessionNativeInterface.ARSessionShouldAttemptRelocalization = true;

            var config = m_ARCameraManager.sessionConfiguration;
            config.worldMap = worldMap;
            UnityARSessionRunOption runOption = UnityARSessionRunOption.ARSessionRunOptionRemoveExistingAnchors | UnityARSessionRunOption.ARSessionRunOptionResetTracking;

            Debug.Log("Restarting session with worldMap");
            session.RunWithConfigAndOptions(config, runOption);
        }

    }
}
