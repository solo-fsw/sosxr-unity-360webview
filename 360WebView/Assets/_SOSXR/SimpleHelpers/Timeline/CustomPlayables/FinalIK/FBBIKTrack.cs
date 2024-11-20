// using RootMotion.FinalIK;

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


/// <summary>
///     Adapted from GameDevGuide: https://youtu.be/12bfRIvqLW4
/// </summary>
[TrackColor(0.818f, 0.581f, 0.468f)]
// [TrackBindingType(typeof(FullBodyBipedIK))]
[TrackClipType(typeof(FBBIKClip))]
// Bind to whatever I need to have in the Timeline
// Tell the track that it can create clips from this binding
public class FBBIKTrack : TrackAssetWithTLClipAccess
{
    protected override void ClipCollector(GameObject gameObject, TimelineClip clip)
    {
        var currentClip = (FBBIKClip) clip.asset;
        currentClip.TimelineClip = clip;

        // currentClip.Template.TrackBinding = (FullBodyBipedIK) gameObject.GetComponent<PlayableDirector>().GetGenericBinding(this); // provides the playable asset with reference to the Rig binding on the track.
    }


    public override Playable
        CreateTrackMixer(PlayableGraph graph, GameObject go,
                         int inputCount) // Tell our track to use the trackMixer to control our playableBehaviours
    {
        return ScriptPlayable<FBBIKTrackMixer>.Create(graph, inputCount);
    }
}