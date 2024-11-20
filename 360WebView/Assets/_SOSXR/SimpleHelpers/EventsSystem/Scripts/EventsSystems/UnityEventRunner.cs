using mrstruijk.Events;
using SOSXR.EnhancedLogger;
using UnityEngine;
using UnityEngine.Events;


public class UnityEventRunner : MonoBehaviour
{
    [SerializeField] private UnityEvent m_unityEvent;


    [Space(20)]
    [SerializeField] private bool m_invokeOnSceneHasBeenLoadedOnAllClients;
    [SerializeField] private bool m_invokeOnAwake;
    [SerializeField] private bool m_invokeOnEnable;
    [SerializeField] private bool m_invokeOnStart;
    [SerializeField] private bool m_invokeOnUpdate;
    [SerializeField] private bool m_invokeOnFixedUpdate;
    [SerializeField] private bool m_invokeOnDisable;
    [SerializeField] private bool m_invokeOnDestroy;
    [SerializeField] private bool m_invokeOnSceneHasBeenUnloadedOnAllClients;

    [Space(20)]
    [SerializeField] [Range(0f, 60f)] private float m_fireDelay = 0f;


    private void FireEventDelayed()
    {
        Invoke(nameof(FireEvent), m_fireDelay);
    }


    private void FireEvent()
    {
        m_unityEvent?.Invoke();
        this.Success("UnityEventRunner: FireEvent");
    }


    private void Awake()
    {
        if (!m_invokeOnAwake)
        {
            return;
        }

        FireEventDelayed();
    }


    private void OnEnable()
    {
        if (m_invokeOnSceneHasBeenLoadedOnAllClients)
        {
            EventsSystem.SceneHasBeenLoadedOnAllClients += FireEventDelayed;
        }

        if (m_invokeOnSceneHasBeenUnloadedOnAllClients)
        {
            EventsSystem.SceneHasBeenUnloadedOnAllClients += FireEventDelayed;
        }

        if (!m_invokeOnEnable)
        {
            return;
        }

        FireEventDelayed();
    }


    private void FireEventDelayed(string sceneName)
    {
        FireEventDelayed();
    }


    private void Start()
    {
        if (!m_invokeOnStart)
        {
            return;
        }

        FireEventDelayed();
    }


    private void Update()
    {
        if (!m_invokeOnUpdate)
        {
            return;
        }

        FireEventDelayed();
    }


    private void FixedUpdate()
    {
        if (!m_invokeOnFixedUpdate)
        {
            return;
        }

        FireEventDelayed();
    }


    private void OnDisable()
    {
        if (m_invokeOnSceneHasBeenLoadedOnAllClients)
        {
            EventsSystem.SceneHasBeenLoadedOnAllClients -= FireEventDelayed;
        }

        if (m_invokeOnSceneHasBeenUnloadedOnAllClients)
        {
            EventsSystem.SceneHasBeenUnloadedOnAllClients -= FireEventDelayed;
        }

        if (!m_invokeOnDisable)
        {
            return;
        }

        FireEventDelayed();
    }


    private void OnDestroy()
    {
        if (!m_invokeOnDestroy)
        {
            return;
        }

        FireEventDelayed();
    }
}