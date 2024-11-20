using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


/// <summary>
///     This creates the TrackMixer, and sets the name of the Clip.
///     Adapted from GameDevGuide: https://youtu.be/12bfRIvqLW4
/// </summary>
[TrackBindingType(typeof(Light))]
[TrackClipType(typeof(LightsClip))] // Bind to whatever you need to control in Timeline
// Tell the track that it can create clips from said binding
public class LightsTrack : TrackAssetWithTLClipAccess
{
    protected override void ClipCollector(GameObject gameObject, TimelineClip clip)
    {
        var currentClip = (LightsClip) clip.asset;

        currentClip.Template.TrackBinding =
            (Light) gameObject.GetComponent<PlayableDirector>()
                              .GetGenericBinding(
                                  this); // provides the playable asset with reference to the Rig binding on the track.
    }


    /// <summary>
    ///     This tells our track to use the trackMixer to control our playableBehaviours
    /// </summary>
    /// <param name="graph"></param>
    /// <param name="go"></param>
    /// <param name="inputCount"></param>
    /// <returns></returns>
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<LightsTrackMixer>.Create(graph, inputCount);
    }
}