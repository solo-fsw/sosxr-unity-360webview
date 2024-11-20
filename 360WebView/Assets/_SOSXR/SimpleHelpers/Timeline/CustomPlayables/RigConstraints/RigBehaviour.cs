using System;
using UnityEngine;
using UnityEngine.Playables;
//using UnityEngine.Animations.Rigging;
using Object = UnityEngine.Object;


/// <summary>
///     Act as our data for the clip to write to
///     Adapted from GameDevGuide: https://youtu.be/12bfRIvqLW4
/// </summary>
[Serializable]
public class RigBehaviour : PlayableBehaviour
{
    public RigClip RigClip;
    public bool Reset;
    public float ResetToValue;
    public Transform WorldPosTarget;
    public Transform WorldRotTarget;
    public Transform RigTarget;
    public AnimationCurve RigWeight;
//    public Rig TrackBinding;
    public Object TrackBinding;
    private float _time;


    public override void
        ProcessFrame(Playable playable, FrameData info,
                     object playerData) // Tell playable what to do when the playhead is on this clip
    {
        if (!Application.isPlaying)
        {
            return;
        }

        // var data = (Rig) playerData; // The playerData is the object that our track is bound to, so cast to the binding of the Track
        var
            data = (Object) playerData; // The playerData is the object that our track is bound to, so cast to the binding of the Track

        if (!data)
        {
            return;
        }

        if (TrackBinding == null)
        {
            TrackBinding = data;
        }

        _time += Time.deltaTime;
        //TrackBinding.weight = RigWeight.Evaluate((float) (_time / RigClip.TimelineClip.duration)) * info.weight; // to set the value

        if (WorldPosTarget == null || WorldRotTarget == null)
        {
            return;
        }

        RigTarget.position = WorldPosTarget.position;
        RigTarget.rotation = WorldRotTarget.rotation;
    }


    /// <summary>
    ///     Also run when playhead moves off clip
    /// </summary>
    /// <param name="playable"></param>
    /// <param name="info"></param>
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (TrackBinding == null)
        {
            return;
        }

        if (RigClip == null)
        {
            return;
        }

        if (Reset)
        {
            //TrackBinding.weight = ResetToValue;
        }
    }
}