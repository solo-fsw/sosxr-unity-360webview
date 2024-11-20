using UnityEngine;


public class DrawGizmoWireCubeOnTransform : DrawGizmosBase
{
    [SerializeField] private float m_scale = 0.05f;


    protected override void SetGizmoDetails()
    {
        Gizmos.matrix = m_startTransform.localToWorldMatrix;

        Gizmos.DrawWireCube(Vector3.zero,
            Vector3.one *
            m_scale); // Because we're already setting the Matrix to the localToWorldMatrix, we can just draw a cube at the origin
    }
}