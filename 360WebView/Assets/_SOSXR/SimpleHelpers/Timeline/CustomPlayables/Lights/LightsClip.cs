using UnityEngine;
using UnityEngine.Playables;


/// <summary>
///     These variables allow us to set the value in the editor.
///     Adapted from GameDevGuide: https://youtu.be/12bfRIvqLW4
/// </summary>
public class LightsClip : PlayableAsset
{
    public LightsBehaviour Template = new();


    /// <summary>
    ///     Here we write our logic for creating the playable behaviour
    /// </summary>
    /// <param name="graph"></param>
    /// <param name="owner"></param>
    /// <returns></returns>
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<LightsBehaviour>.Create(graph); // Create a playable using the constructor

        var behaviour = playable.GetBehaviour(); // Get behaviour

        return playable;
    }
}