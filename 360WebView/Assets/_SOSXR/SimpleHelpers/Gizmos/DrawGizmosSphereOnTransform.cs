using UnityEngine;


public class DrawGizmosSphereOnTransform : DrawGizmosBase
{
    [Tooltip("Radius works if Transform Scale is uniform")]
    [SerializeField] private float m_radius = 0.05f;


    protected override void SetGizmoDetails()
    {
        Gizmos.DrawSphere(m_startTransform.position, m_radius * m_startTransform.localScale.x);
    }
}