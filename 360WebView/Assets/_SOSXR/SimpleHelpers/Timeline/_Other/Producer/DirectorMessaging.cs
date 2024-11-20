using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;


public class DirectorMessaging : MonoBehaviour
{
    public UnityEvent Playing;
    public UnityEvent Paused;
    public UnityEvent Stopped;

    [Tooltip("No need to set manually: Is set via DirectorControlButtonSpawner")]
    [ReadOnly] public PlayableDirector Director;


    private void Start()
    {
        RegisterDirectors();
    }


    /// <summary>
    ///     These are registered in Start, because otherwise they'd be deregistered when disabling the UI component
    /// </summary>
    private void RegisterDirectors()
    {
        if (Director == null)
        {
            return;
        }

        Director.played += DirectorPlay;
        Director.paused += DirectorPause;
        Director.stopped += DirectorStop;
    }


    private void DirectorPlay(PlayableDirector playableDirector)
    {
        if (playableDirector != Director)
        {
            return;
        }

        Playing?.Invoke();
    }


    private void DirectorPause(PlayableDirector playableDirector)
    {
        if (playableDirector != Director)
        {
            return;
        }

        Paused?.Invoke();
    }


    private void DirectorStop(PlayableDirector playableDirector)
    {
        if (playableDirector != Director)
        {
            return;
        }

        Stopped?.Invoke();
    }


    private void OnDestroy()
    {
        UnregisterDirectors();
    }


    /// <summary>
    ///     These are deregistered in OnDestroy, because otherwise they'd be deregistered when disabling the UI component
    /// </summary>
    private void UnregisterDirectors()
    {
        if (Director == null)
        {
            return;
        }

        Director.played -= DirectorPlay;
        Director.paused -= DirectorPause;
        Director.stopped -= DirectorStop;
    }
}