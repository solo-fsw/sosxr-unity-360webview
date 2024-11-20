using System;
using mrstruijk.Extensions;
using UnityEngine;
using UnityEngine.Playables;


/// <summary>
///     Acts as our data for the clip to write to
///     Adapted from GameDevGuide: https://youtu.be/12bfRIvqLW4
/// </summary>
[Serializable]
public class RigidbodyBehaviour : PlayableBehaviour
{
    public ExposedReference<Transform> TargetRef;
    public Rigidbody TrackBinding;
    public bool IsKinematic;
    public bool UseGravity;
    public bool AddForce;
    public float Amount;
    public Transform Target;
    public ForceMode ForceMode;

    private bool _behaviourCompleted;
    private Vector3 _direction;
    private Vector3 _displacement;


    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (TrackBinding == null && playerData != null)
        {
            TrackBinding = playerData as Rigidbody;
        }

        if (!Application.isPlaying)
        {
            return;
        }

        if (_behaviourCompleted)
        {
            return;
        }

        if (TrackBinding == null)
        {
            return;
        }

        TrackBinding.isKinematic = IsKinematic;
        TrackBinding.useGravity = UseGravity;

        if (AddForce && Target != null)
        {
            _displacement = TrackBinding.transform.CalculateDisplacement(Target, new Vector3(1, 1, 1));
            _direction = _displacement.CalculateDirection();

            TrackBinding.AddForce(_direction * Amount, ForceMode);
        }

        _behaviourCompleted = true;
    }
}