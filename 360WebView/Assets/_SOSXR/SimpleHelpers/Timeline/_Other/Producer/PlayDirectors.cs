using System.Collections.Generic;
using System.Threading.Tasks;
using mrstruijk.Events;
using mrstruijk.Extensions;
using SOSXR.EnhancedLogger;
using UnityEngine;
using UnityEngine.Playables;


public class PlayDirectors : MonoBehaviour
{
    [Tooltip("These will play in order that they're listed here")]
    [SerializeField]
    public List<PlayableDirector> m_directors;

    [SerializeField] [Range(0f, 10f)] private float m_playDelay = 5f;

    public int CurrentDirectorIndex { get; set; } = -1;

    public bool IsPlaying => CurrentDirectorIndex != -1 && m_directors[CurrentDirectorIndex].state == PlayState.Playing;

    public double PlayingTime => IsPlaying ? m_directors[CurrentDirectorIndex].time : 0;


    private void OnEnable()
    {
        foreach (var director in m_directors)
        {
            director.stopped += PlayNextDirector;
        }

        EventsSystem.SceneHasBeenLoadedOnAllClients += OrganisedPlaying;
    }


    private async void OrganisedPlaying(string sceneName)
    {
        await Task.Delay((int) (m_playDelay * 1000));

        SendPlayCommandLocally();
    }


    public void SendPlayCommandLocally(int index = 0, double startTime = 0)
    {
        CurrentDirectorIndex = index;
        m_directors[CurrentDirectorIndex].time = startTime;
        m_directors[CurrentDirectorIndex].Play();
        this.Success("We're a client, and we're playing the director", index, "Starting at time", startTime);
    }


    private void PlayNextDirector(PlayableDirector previousDirector)
    {
        if (previousDirector == null)
        {
            Log.Debug("PlayDirector", "previousDirector is null");

            return;
        }

        if (!m_directors.ContainsIndex(CurrentDirectorIndex))
        {
            this.Warning("List", nameof(m_directors), "doesn't have index", CurrentDirectorIndex);

            return;
        }

        if (previousDirector != m_directors[CurrentDirectorIndex])
        {
            this.Warning(
                "The CurrentDirectorIndex seems to be off. We're getting a 'Stopped' event from a director that's not the current director. We will go ahead, with the 'CurrentDirectorIndex + 1' director, but this is not ideal");

            this.Warning("StoppedCommand came from", previousDirector.name, "while the CurrentDirectorIndex is",
                CurrentDirectorIndex, "and the director at that index is", m_directors[CurrentDirectorIndex].name);
        }


        var nextIndex = CurrentDirectorIndex + 1;

        if (nextIndex >= m_directors.Count)
        {
            this.Success(
                "We're a server, and we're not going to play the next director, because we're at the end of the list");

            return;
        }

        SendPlayCommandLocally(nextIndex);
    }


    public void SendStopCommandToClientRpc()
    {
        var count = 0;

        foreach (var director in m_directors)
        {
            director.Stop();
            count++;
        }

        this.Success("We're a client, and we're stopping all directors. We stopped", count, "directors");
    }


    private void OnDisable()
    {
        Cleanup();
    }


    public void Cleanup()
    {
        foreach (var director in m_directors)
        {
            director.stopped -= PlayNextDirector;
        }

        EventsSystem.SceneHasBeenLoadedOnAllClients -= OrganisedPlaying;
    }
}