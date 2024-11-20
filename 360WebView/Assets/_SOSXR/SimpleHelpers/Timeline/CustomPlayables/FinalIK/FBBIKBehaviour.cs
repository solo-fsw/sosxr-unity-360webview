using System;
using SOSXR.EnhancedLogger;
using UnityEngine;
using UnityEngine.Playables;
// using RootMotion.FinalIK;
using Object = UnityEngine.Object;


[Serializable]
public class FBBIKBehaviour : PlayableBehaviour
{
    public AnimationCurve RigWeight = new(new Keyframe(0, 1), new Keyframe(1, 1));

    public bool Reset = true;
    public float ResetToValue;
    public bool IsLeft;

    private float _time;

    public FBBIKClip Clip { get; set; }
    public Transform WorldPosTarget { get; set; }
    public Transform RigTarget { get; set; }
    // public FullBodyBipedIK TrackBinding { get; set; }
    public Object TrackBinding { get; set; }


    public override void
        ProcessFrame(Playable playable, FrameData info,
                     object playerData) // Tell playable what to do when the playhead is on this clip
    {
        if (!Application.isPlaying)
        {
            return;
        }

        // var data = (FullBodyBipedIK) playerData; // The playerData is the object that our track is bound to, so cast to the binding of the Track
        var
            data = (Object) playerData; // The playerData is the object that our track is bound to, so cast to the binding of the Track

        if (!data)
        {
            Log.Error("FBBIKBehaviour", "No data");

            return;
        }

        if (TrackBinding == null)
        {
            TrackBinding = data;
        }

        _time += Time.deltaTime;

        if (TrackBinding == null)
        {
            Log.Error("TRACKBINDING == NULL");

            return;
        }

        if (Clip == null)
        {
            Log.Error("CLIP == null");

            return;
        }

        if (Clip.TimelineClip == null)
        {
            Log.Error("CLIP.TIMELINECLIP == null");

            return;
        }

        if (IsLeft)
        {
            //TrackBinding.solver.leftHandEffector.positionWeight = RigWeight.Evaluate((float) (_time / Clip.TimelineClip.duration)) * info.weight; // to set the value
        }

        // TrackBinding.solver.rightHandEffector.positionWeight = RigWeight.Evaluate((float) (_time / Clip.TimelineClip.duration)) * info.weight; // to set the value
        if (WorldPosTarget == null)
        {
            return;
        }

        RigTarget.position = WorldPosTarget.position;
    }


    /// <summary>
    ///     Also run when playhead moves off clip
    /// </summary>
    /// <param name="playable"></param>
    /// <param name="info"></param>
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (!Application.isPlaying)
        {
            return;
        }

        _time = 0f;

        if (TrackBinding == null)
        {
            return;
        }

        if (Clip == null)
        {
            return;
        }

        if (!Reset)
        {
            return;
        }

        if (IsLeft)
        {
            // TrackBinding.solver.leftHandEffector.positionWeight = ResetToValue;
        }
        // TrackBinding.solver.rightHandEffector.positionWeight = ResetToValue;
    }
}