using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


/// <summary>
///     This class us to set or see the values in the editor.
/// </summary>
[Serializable]
public class ToTargetClip : PlayableAsset
{
    public ExposedReference<GameObject> startingPoint;

    public ExposedReference<GameObject> target;

    public ToTargetBehaviour template = new();

    private TimelineClip _timelineClip;
    private const string Divider = " - ";
    public GameObject StartingPoint { get; set; }
    public GameObject Target { get; set; }

    public TimelineClip TimelineClip
    {
        get => _timelineClip;
        set => _timelineClip = value;
    }


    public override double duration
    {
        get
        {
            if (template == null || template.forceClipLength == false || template.startingDurationToTarget == 0 ||
                TimelineClip == null)
            {
                return PlayableBinding.DefaultDuration;
            }

            return TimelineClip.duration = template.startingDurationToTarget;
        }
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
            ScriptPlayable<ToTargetBehaviour>.Create(graph, template); // Create a playable using the constructor

        template = playable.GetBehaviour(); // Get behaviour, and set as template

        if (StartingPoint == null)
        {
            StartingPoint = startingPoint.Resolve(graph.GetResolver());
        }

        if (Target == null)
        {
            Target = target.Resolve(graph.GetResolver());
        }

        SetValuesOnBehaviourFromClip(template);

        SetDisplayName(template, TimelineClip);

        return playable;
    }


    private void SetValuesOnBehaviourFromClip(ToTargetBehaviour behaviour)
    {
        behaviour.toTargetClip = this;
        behaviour.Target = Target;
        behaviour.StartingPoint = StartingPoint;
    }


    /// <summary>
    ///     The displayname of the clip in Timeline will be set using this method.
    ///     Amended from: https://forum.unity.com/threads/change-clip-name-with-custom-playable.499311/
    /// </summary>
    private void SetDisplayName(ToTargetBehaviour behaviour, TimelineClip clip)
    {
        var displayName = "";

        if (behaviour.Target == null || behaviour.StartingPoint == null)
        {
            return;
        }

        displayName += "To: " + behaviour.Target.name;

        displayName += Divider + behaviour.StartingPoint.name; // TODO: fix naming

        displayName = CustomPlayableClipHelper.RemoveTrailingDivider(displayName);
        displayName = CustomPlayableClipHelper.SetDisplayNameIfStillEmpty(displayName, "New ToTarget Clip");

        if (clip == null)
        {
            return;
        }

        clip.displayName = displayName;
    }
}