using System;
using UnityEngine;
using UnityEngine.Playables;


[Serializable]
public class TLActivateBehaviour : PlayableBehaviour
{
    public ExposedReference<GameObject> activateReference;

    public bool activateAtStart;
    public bool activateAtEnd;
    private bool _activatedAtEnd;
    private bool _activatedAtStart;

    private bool _clipHasPlayed;
    public ITLActivate Activate;


    public override void OnPlayableCreate(Playable playable)
    {
        var director = playable.GetGraph().GetResolver() as PlayableDirector;

        if (director == null)
        {
            return;
        }

        if (activateReference.Resolve(director) == null)
        {
            return;
        }

        if (activateReference.Resolve(director).GetComponent<ITLActivate>() == null)
        {
            Debug.LogWarningFormat("Selected GameObject {0} does not have an ITLActivate component, cannot proceed",
                activateReference.Resolve(director).name);

            return;
        }

        Activate = activateReference.Resolve(director).GetComponent<ITLActivate>();
    }


    public override void OnGraphStart(Playable playable)
    {
        _activatedAtStart = false;
        _activatedAtEnd = false;
    }


    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (!Application.isPlaying)
        {
            return;
        }

        _clipHasPlayed = true;

        if (activateAtStart == false)
        {
            return;
        }

        if (_activatedAtStart == false)
        {
            Activate?.TLActivate();
            _activatedAtStart = true;
        }
    }


    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (!Application.isPlaying)
        {
            return;
        }

        if (_clipHasPlayed == false)
        {
            return;
        }

        if (activateAtEnd == false)
        {
            return;
        }

        if (_activatedAtEnd == false)
        {
            Activate?.TLActivate();
            _activatedAtEnd = true;
        }
    }
}