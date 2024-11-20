using System.Linq;
using mrstruijk.Events;
using SOSXR.EnhancedLogger;
using UnityEngine;
using UnityEngine.Events;


public class OnAnySceneLoad : MonoBehaviour
{
    [SerializeField] private UnityEvent m_onSceneLoad;

    [SerializeField] private string[] m_exceptScenes = {"0. Connection"};


    private void OnEnable()
    {
        EventsSystem.SceneHasBeenLoadedOnAllClients += OnSceneLoad;
    }


    private void OnSceneLoad(string loadedSceneName)
    {
        if (m_exceptScenes.Contains(loadedSceneName))
        {
            return;
        }

        m_onSceneLoad?.Invoke();

        this.Success("OnSceneLoad UnityEvent has been called");
    }


    private void OnDisable()
    {
        EventsSystem.SceneHasBeenLoadedOnAllClients -= OnSceneLoad;
    }
}