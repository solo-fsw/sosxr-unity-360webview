using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


/// <summary>
///     Adapted from GameDevGuide: https://youtu.be/12bfRIvqLW4
/// </summary>
public class RigClip : PlayableAsset
{
    public ExposedReference<Transform> rigTargetRef;
    public ExposedReference<Transform> worldPosTargetRef;
    public ExposedReference<Transform> worldRotTargetRef;

    public RigBehaviour Template;

    public TimelineClip TimelineClip { get; set; }


    public override Playable
        CreatePlayable(PlayableGraph graph,
                       GameObject owner) // Here we write our logic for creating the playable behaviour
    {
        var playable = ScriptPlayable<RigBehaviour>.Create(graph, Template); // Create a playable, using the constructor

        var behaviour = playable.GetBehaviour(); // Get behaviour

        if (Template.RigTarget == null && rigTargetRef.Resolve(graph.GetResolver()) != null)
        {
            Template.RigTarget = rigTargetRef.Resolve(graph.GetResolver());
        }

        if (Template.WorldPosTarget == null && worldPosTargetRef.Resolve(graph.GetResolver()) != null)
        {
            Template.WorldPosTarget = worldPosTargetRef.Resolve(graph.GetResolver());
        }

        if (Template.WorldRotTarget == null && worldRotTargetRef.Resolve(graph.GetResolver()) != null)
        {
            Template.WorldRotTarget = worldRotTargetRef.Resolve(graph.GetResolver());
        }

        behaviour.RigClip = this;

        return playable;
    }
}