using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.Timeline;


public abstract class TrackAssetWithTLClipAccess : TrackAsset
{
    protected sealed override Playable CreatePlayable(PlayableGraph graph, GameObject gameObject, TimelineClip clip)
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

        ClipCollector(gameObject, clip);

        return handle;
    }


    protected abstract void ClipCollector(GameObject gameObject, TimelineClip clip);
}