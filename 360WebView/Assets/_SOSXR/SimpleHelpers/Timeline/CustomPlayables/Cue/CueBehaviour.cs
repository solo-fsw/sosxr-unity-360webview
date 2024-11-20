using System;
using UnityEngine;
using UnityEngine.Playables;


[Serializable]
public class CueBehaviour : PlayableBehaviour
{
    public ExposedReference<ObjectCue> Reference;

    public bool CueAtStart;
    public bool CueAtEnd;

    public ObjectCue ObjectCue;
    private bool _clipHasPlayed;
    private bool _cueuedAtEnd;
    private bool _cueuedAtStart;


    public override void OnPlayableCreate(Playable playable)
    {
        var director = playable.GetGraph().GetResolver() as PlayableDirector;

        if (director == null)
        {
            return;
        }

        if (Reference.Resolve(director) == null)
        {
            return;
        }

        ObjectCue = Reference.Resolve(director);
    }


    public override void OnGraphStart(Playable playable)
    {
        _cueuedAtStart = false;
        _cueuedAtEnd = false;
    }


    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (!Application.isPlaying)
        {
            return;
        }

        _clipHasPlayed = true;

        if (!CueAtStart)
        {
            return;
        }

        if (_cueuedAtStart)
        {
            return;
        }

        // ObjectCue.StartCue();
        _cueuedAtStart = true;
    }


    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (!Application.isPlaying)
        {
            return;
        }

        if (!_clipHasPlayed)
        {
            return;
        }

        if (!CueAtEnd)
        {
            return;
        }

        if (_cueuedAtEnd)
        {
            return;
        }

        // ObjectCue.StartCue();
        _cueuedAtEnd = true;
    }
}