using System;
using UnityEngine;
using UnityEngine.Playables;


/// <summary>
///     Acts as our data for the clip to write to
///     Adapted from GameDevGuide: https://youtu.be/12bfRIvqLW4
/// </summary>
[Serializable]
public class ParentingBehaviour : PlayableBehaviour
{
    public GameObject TrackBinding;
    public GameObject ParentToObject;
    public bool ZeroInOnParent;
    public Vector3 LocalPositionOffset;
    public Vector3 LocalRotationOffset;

    private bool _behaviourDone;


    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var
            data = (GameObject) playerData; // The playerData is the object that our track is bound to, so cast to the binding of the Track

        if (!data)
        {
            return;
        }

        if (TrackBinding == null)
        {
            TrackBinding = data;
        }

        if (!Application.isPlaying)
        {
            return;
        }

        if (_behaviourDone)
        {
            return;
        }

        if (ParentToObject == null)
        {
            TrackBinding.transform.parent = null;
            _behaviourDone = true;

            return;
        }

        if (TrackBinding.transform.parent != ParentToObject.transform)
        {
            TrackBinding.transform.parent = ParentToObject.transform;
        }

        if (ZeroInOnParent)
        {
            TrackBinding.transform.localPosition = Vector3.zero;
            TrackBinding.transform.localRotation = new Quaternion();
        }

        if (LocalPositionOffset != Vector3.zero)
        {
            TrackBinding.transform.localPosition = LocalPositionOffset;
        }

        if (LocalRotationOffset != Vector3.zero)
        {
            TrackBinding.transform.localRotation = Quaternion.Euler(LocalRotationOffset);
        }

        _behaviourDone = true;
    }
}