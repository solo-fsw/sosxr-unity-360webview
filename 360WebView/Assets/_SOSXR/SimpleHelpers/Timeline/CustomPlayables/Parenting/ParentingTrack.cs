using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.Timeline;


/// <summary>
///     Adapted from GameDevGuide: https://youtu.be/12bfRIvqLW4
/// </summary>
[TrackColor(0.468f, 0.704f, 0.818f)]
[TrackBindingType(typeof(GameObject))]
[TrackClipType(typeof(ParentingClip))]
[Serializable]
// Bind to whatever you need to have in the Timeline
// Tell the track that it can create clips from this binding
public class ParentingTrack : TrackAsset
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

        var currentClip = (ParentingClip) clip.asset;

        currentClip.Template.TrackBinding =
            (GameObject) gameObject.GetComponent<PlayableDirector>().GetGenericBinding(this);

        return handle;
    }
}