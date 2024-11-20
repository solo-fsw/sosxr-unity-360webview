using System.Linq;
using mrstruijk.Events;
using SOSXR.EnhancedLogger;
using UnityEngine;
using UnityEngine.Events;


public class OnAnySceneUnload : MonoBehaviour
{
    [SerializeField] private UnityEvent m_onSceneUnload;

    [SerializeField] private string[] m_exceptScenes = {"0. Connection"};


    private void OnEnable()
    {
        EventsSystem.SceneHasBeenUnloadedOnAllClients += OnSceneUnload;
    }


    private void OnSceneUnload(string loadedSceneName)
    {
        if (m_exceptScenes.Contains(loadedSceneName))
        {
            return;
        }

        m_onSceneUnload?.Invoke();

        this.Success("OnSceneUnload Unity event has been called");
    }


    private void OnDisable()
    {
        EventsSystem.SceneHasBeenUnloadedOnAllClients -= OnSceneUnload;
    }
}