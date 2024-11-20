using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


[Serializable]
public class LooperClip : PlayableAsset
{
    public LooperBehaviour behaviour;

    public TimelineClip TimelineClip { get; set; }


    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<LooperBehaviour>.Create(graph, behaviour);

        behaviour = playable.GetBehaviour(); // Set it directly to the behaviour

        return playable;
    }
}