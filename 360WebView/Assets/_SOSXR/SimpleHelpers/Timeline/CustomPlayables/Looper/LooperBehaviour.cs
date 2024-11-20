using System;
using mrstruijk;
using SOSXR.EnhancedLogger;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


[Serializable]
public class LooperBehaviour : PlayableBehaviour
{
    public bool handControlTo;

    public ExposedReference<LoopBreaker> loopBreakerReference;

    public LooperState
        startLooperState; // This is what you set in the inspector for what this clip initially needs to do
    public LooperState
        runningLooperState; // This allows us to revert back to choice made in inspector: otherwise this ScriptableObject will store the changes made in PlayMode
    [SerializeField] [DisableEditing] private double _startTime;
    [SerializeField] [DisableEditing] private double _endTime;

    private bool _behaviourHasStarted;

    private PlayableDirector _director;
    private LooperState _oldLooperState; // This is to check whether we need to redraw the clip name during PlayMode


    public LoopBreaker LoopBreakerBase { get; private set; }

    public TimelineClip TimelineClip { get; set; }

    public LooperClip LooperClip { get; set; }
    public object ClipName { get; set; }


    public override void OnPlayableCreate(Playable playable)
    {
        _director = playable.GetGraph().GetResolver() as PlayableDirector;

        if (_director == null)
        {
            return;
        }

        GetAndInitialiseLoopBreakerBase();
    }


    private void GetAndInitialiseLoopBreakerBase()
    {
        if (handControlTo == false)
        {
            return;
        }

        LoopBreakerBase = loopBreakerReference.Resolve(_director);

        if (LoopBreakerBase == null)
        {
            return;
        }

        LoopBreakerBase.Init(this);
    }


    public override void OnGraphStart(Playable playable)
    {
        SetRunningLooperState();

        StoreStartEndTimes();
    }


    private void SetRunningLooperState()
    {
        runningLooperState = startLooperState;
    }


    private void StoreStartEndTimes()
    {
        if (TimelineClip == null)
        {
            Log.Error("LooperBehaviour", "Timelineclip is null, cannot store start/end times");

            return;
        }

        _startTime = TimelineClip.start;
        _endTime = TimelineClip.end;

        Log.Debug("LooperBehaviour", "Stored start time", _startTime, "and end time", _endTime);
    }


    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        _behaviourHasStarted = true;

        if (LoopBreakerBase == null)
        {
            return;
        }

        LoopBreakerBase.SetCurrentLooper(this);

        LoopBreakerBase.TakeControl();
    }


    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (runningLooperState == LooperState.Looping)
        {
            return;
        }

        if (runningLooperState == LooperState.GoToStart)
        {
            Log.Debug("LooperBehaviour", "GoToStart");
            _director.time = _startTime;
        }
        else if (runningLooperState == LooperState.GoToEnd)
        {
            Log.Debug("LooperBehaviour", "GoToEnd");
            Log.Debug("LooperBehaviour", "The time is now", _endTime);
            _director.time = _endTime;
        }

        // runningLooperState = LooperState.DoNotLoop;
    }


    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (_director == null)
        {
            return;
        }

        if (_director.time <=
            _startTime) // REQUIRED CHECK! OnBehaviourPause also runs right after OnGraphStart, so before this clip has actually played.
        {
            return;
        }

        if (_behaviourHasStarted ==
            false) // Backup check: making sure that the clip has started playing, therefore the below code doesn't run prior to clip start.
        {
            return;
        }

        if (runningLooperState != LooperState.Looping)
        {
            return;
        }

        _director.time = _startTime;
        _behaviourHasStarted = false;
    }


    public override void OnGraphStop(Playable playable)
    {
        SetRunningLooperState();
    }
}