using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


public class FBBIKClip : PlayableAsset
{
    public ExposedReference<Transform> RigTargetRef;
    public ExposedReference<Transform> WorldPosTargetRef;


    public FBBIKBehaviour Template = new();
    public TimelineClip TimelineClip { get; set; }


    public override Playable
        CreatePlayable(PlayableGraph graph,
                       GameObject owner) // Here we write our logic for creating the playable behaviour
    {
        var playable =
            ScriptPlayable<FBBIKBehaviour>.Create(graph, Template); // Create a playable, using the constructor

        var behaviour = playable.GetBehaviour(); // Get behaviour
        behaviour.Clip = this;
        behaviour.RigTarget = RigTargetRef.Resolve(graph.GetResolver());
        behaviour.WorldPosTarget = WorldPosTargetRef.Resolve(graph.GetResolver());

        /*
        Template.Clip = this;
        Template.RigTarget = RigTargetRef.Resolve(graph.GetResolver());
        Template.WorldPosTarget = WorldPosTargetRef.Resolve(graph.GetResolver());
        */

        return playable;
    }
}