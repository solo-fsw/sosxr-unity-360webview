using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


/// <summary>
///     Allows us to set the values in the editor
///     Adapted from GameDevGuide: https://youtu.be/12bfRIvqLW4
/// </summary>
[Serializable]
public class RigidbodyClip : PlayableAsset
{
    public RigidbodyBehaviour Template;
    //[ShowIf(nameof(addForce))]
    // public ExposedReference<Transform> target;
    // public bool isKinematic;
    // public bool useGravity;
    // public bool addForce;
    // [ShowIf(nameof(addForce))] public float amount;
    // [ShowIf(nameof(addForce))] public ForceMode forceMode;


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
            ScriptPlayable<RigidbodyBehaviour>.Create(graph, Template); // Create a playable, using the constructor

        var behaviour = playable.GetBehaviour(); // Get behaviour


        //SetValuesOnBehaviourFromClip(behaviour);
        // SetDisplayName(TimelineClip);

        return playable;
    }


/*
    /// <summary>
    ///     The displayname of the clip in Timeline will be set using this method.
    ///     Amended from: https://forum.unity.com/threads/change-clip-name-with-custom-playable.499311/
    /// </summary>
    private void SetDisplayName(TimelineClip clip)
    {
        if (clip == null)
        {
            return;
        }

        clip.displayName = "";

        if (isKinematic)
        {
            clip.displayName += "Kinematic" + " & ";
        }
        else
        {
            clip.displayName += "Non-Kinematic" + " & ";
        }

        if (useGravity)
        {
            clip.displayName += "Gravity" + " & ";
        }

        if (addForce)
        {
            clip.displayName += "Force: [" + amount + "]" + " (" + Target.name + ")";
        }

        if (clip.displayName.EndsWith(" & "))
        {
            clip.displayName = clip.displayName.Remove(clip.displayName.Length - 3);
        }
    }
    */
}