using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


[Serializable]
public abstract class AnimatorClip : PlayableAsset
{
    [HideInInspector] public AnimatorBehaviour template = new();
    private TimelineClip _timelineClip;

    public TimelineClip TimelineClip
    {
        get => _timelineClip;
        set => _timelineClip = value;
    }


    /// <summary>
    ///     Here we write our logic for creating the playable behaviour
    /// </summary>
    /// <param name="graph"></param>
    /// <param name="owner"></param>
    /// <returns></returns>
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable =
            ScriptPlayable<AnimatorBehaviour>.Create(graph, template); // Create a playable using the constructor

        var behaviour = playable.GetBehaviour(); // Get behaviour

        SetClipOnBehaviour(behaviour);

        FindIndexes(behaviour);

        SetValuesOnBehaviourFromClip(behaviour);

        SetClipDuration(TimelineClip);

        SetOverallDisplayName(TimelineClip);

        return playable;
    }


    public abstract void FindIndexes(AnimatorBehaviour animatorBehaviour);


    protected abstract void SetClipOnBehaviour(AnimatorBehaviour animatorBehaviour);


    /// <summary>
    ///     Here we set the clip duration to the length that's set by the values on the clip itself.
    /// </summary>
    /// <param name="clip"></param>
    protected virtual void SetClipDuration(TimelineClip clip)
    {
        // Currently only used in the TriggerClip.
    }


    protected abstract void SetValuesOnBehaviourFromClip(AnimatorBehaviour animatorBehaviour);


    /// <summary>
    ///     The displayname of the clip in Timeline will be set using this method.
    ///     Name is only set if a varable is used (in case of X/Y/Z if they have a value != 0, in other cases if the string
    ///     name of the variable is
    ///     not null).
    ///     Amended from: https://forum.unity.com/threads/change-clip-name-with-custom-playable.499311/
    /// </summary>
    private void SetOverallDisplayName(TimelineClip clip)
    {
        var dispName = SetDisplayName();

        dispName = CustomPlayableClipHelper.RemoveTrailingDivider(dispName);
        dispName = CustomPlayableClipHelper.SetDisplayNameIfStillEmpty(dispName, "New Animator Clip");

        if (clip != null)
        {
            clip.displayName = dispName;
        }
    }


    protected abstract string SetDisplayName();
}