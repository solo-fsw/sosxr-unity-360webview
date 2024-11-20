using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Timeline;


/// <summary>
///     This creates the TrackMixer, and sets the name of the Clip.
///     Adapted from GameDevGuide: https://youtu.be/12bfRIvqLW4
/// </summary>
[TrackColor(0.071f, 0.071f, 0.071f)]
[TrackBindingType(typeof(VolumeProfile))]
[TrackClipType(typeof(LUTClip))]
// Bind to whatever you need to control in Timeline
// Tell the track that it can create clips from said binding
public class LUTTrack : TrackAsset
{
    /// <summary>
    ///     Overwritten because this allows us to send the TimeLineClip over
    /// </summary>
    protected override Playable CreatePlayable(PlayableGraph graph, GameObject gameObject, TimelineClip clip)
    {
        if (!graph.IsValid())
        {
            throw new ArgumentException("graph must be a valid PlayableGraph");
        }

        if (clip == null)
        {
            throw new ArgumentNullException(nameof(clip));
        }

        if (clip.asset is not IPlayableAsset asset)
        {
            return Playable.Null;
        }

        var handle = asset.CreatePlayable(graph, gameObject);

        if (!handle.IsValid())
        {
            return handle;
        }

        handle.SetAnimatedProperties(clip.curves);
        handle.SetSpeed(clip.timeScale);

        var currentClip = (LUTClip) clip.asset;

        return handle;
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
        return ScriptPlayable<LUTTrackMixer>.Create(graph, inputCount);
    }
}