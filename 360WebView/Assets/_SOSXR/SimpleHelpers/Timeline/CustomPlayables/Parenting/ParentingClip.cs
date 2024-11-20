using UnityEngine;
using UnityEngine.Playables;


/// <summary>
///     Allows us to set the values in the editor
///     Adapted from GameDevGuide: https://youtu.be/12bfRIvqLW4
/// </summary>
public class ParentingClip : PlayableAsset
{
    public ExposedReference<GameObject> ParentToObjectReference;

    public ParentingBehaviour Template;

    private PlayableGraph _playableGraph;


    /// <summary>
    ///     Here we write our logic for creating the playable behaviour
    /// </summary>
    /// <param name="graph"></param>
    /// <param name="owner"></param>
    /// <returns></returns>
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable =
            ScriptPlayable<ParentingBehaviour>.Create(graph, Template); // Create a playable, using the constructor

        var behaviour = playable.GetBehaviour(); // Get behaviour

        if (Template.ParentToObject == null && ParentToObjectReference.Resolve(graph.GetResolver()) != null)
        {
            Template.ParentToObject = ParentToObjectReference.Resolve(graph.GetResolver());
        }

        return playable;
    }
}