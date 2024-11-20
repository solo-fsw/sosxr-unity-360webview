using UnityEngine;
using UnityEngine.Playables;


public class CueClip : PlayableAsset
{
    public CueBehaviour Template;


    /// <summary>
    ///     Here we write our logic for creating the playable behaviour
    /// </summary>
    /// <param name="graph"></param>
    /// <param name="owner"></param>
    /// <returns></returns>
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<CueBehaviour>.Create(graph, Template); // Create a playable, using the constructor

        return playable;
    }
}