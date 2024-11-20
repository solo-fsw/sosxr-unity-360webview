using System;
using mrstruijk;
using mrstruijk.Extensions;
using UnityEngine;
using UnityEngine.Playables;


/// <summary>
///     This acts as our data for the clip to write to
/// </summary>
[Serializable]
public class RotateToTargetBehaviour : PlayableBehaviour
{
    public ExposedReference<GameObject> TargetRef;
    public GameObject TrackBinding;

    [Space(20)]
    [Tooltip("Which axis to use for calculations? 0 = don't use, 1 = use")]
    public Vector3Int AxisToUse = new(1, 0, 1);
    public float RotateSpeed = 1.25f;

    [DisableEditing] public Vector3 DisplacementFromTarget;
    [DisableEditing] public Vector3 DirectionToTarget;
    [DisableEditing] public float DistanceToTarget;

    public GameObject Target { get; set; }


    public override void OnGraphStart(Playable playable)
    {
        CalculateValues();
    }


    private void CalculateValues()
    {
        if (Target == null)
        {
            return;
        }

        DisplacementFromTarget = TrackBinding.transform.CalculateDisplacement(Target.transform, AxisToUse);
        DirectionToTarget = DisplacementFromTarget.CalculateDirection();
        DistanceToTarget = DisplacementFromTarget.CalculateDistance();
    }


    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var
            data = (GameObject) playerData; // The playerData is the object that our track is bound to, so cast to the binding of the Track

        if (data == null)
        {
            return;
        }

        if (TrackBinding == null)
        {
            TrackBinding = data;
        }

        CalculateValues();

        DrawRayInRotationDirection();

        HandleSmoothRotation(DirectionToTarget);
    }


    private void DrawRayInRotationDirection()
    {
        TrackBinding.transform.DrawRay(DisplacementFromTarget);
    }


    /// <summary>
    ///     Rotates forward vector to target by speed
    /// </summary>
    /// <param name="direction"></param>
    private void HandleSmoothRotation(Vector3 direction)
    {
        if (!Application.isPlaying)
        {
            return;
        }

        var newDirection =
            Vector3.RotateTowards(TrackBinding.transform.forward, direction, RotateSpeed * Time.deltaTime, 0.0f);

        TrackBinding.transform.rotation = Quaternion.LookRotation(newDirection);
    }
}