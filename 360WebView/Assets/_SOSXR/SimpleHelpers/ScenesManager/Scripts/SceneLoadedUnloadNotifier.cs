using mrstruijk.Events;
using SOSXR.EnhancedLogger;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class SceneLoadedUnloadNotifier : MonoBehaviour
{
    [SerializeField] private UnityEvent m_onSceneLoad;
    [SerializeField] private UnityEvent m_onSceneUnload;
    private bool _loadFired;
    private bool _unloadFired;


    private void Awake()
    {
        EventsSystem.SceneHasBeenLoadedOnAllClients += FireOnSceneLoadEvent;
        EventsSystem.SceneHasBeenUnloadedOnAllClients += FireOnSceneUnLoadEvent;
    }


    private void FireOnSceneLoadEvent(string loadedSceneName)
    {
        if (loadedSceneName == SceneManager.GetSceneAt(SceneManager.sceneCount - 1).name)
        {
            FireSceneLoadEvent();
        }
    }


    private void FireSceneLoadEvent()
    {
        if (_loadFired)
        {
            return;
        }

        m_onSceneLoad?.Invoke();
        this.Info("Fired OnSceneLoadEvent on", gameObject.name);
        _loadFired = true;
        _unloadFired = false;
    }


    private void FireOnSceneUnLoadEvent(string loadedSceneName)
    {
        if (loadedSceneName == SceneManager.GetSceneAt(SceneManager.sceneCount - 1).name)
        {
            FireSceneUnloadEvent();
        }
    }


    private void FireSceneUnloadEvent()
    {
        if (_unloadFired)
        {
            return;
        }

        m_onSceneUnload?.Invoke();
        this.Info("Fired OnSceneUnloadEvent on", gameObject.name);
        _unloadFired = true;
        _loadFired = false;
    }


    private void OnDestroy()
    {
        EventsSystem.SceneHasBeenLoadedOnAllClients -= FireOnSceneLoadEvent;
        EventsSystem.SceneHasBeenUnloadedOnAllClients -= FireOnSceneUnLoadEvent;
    }
}