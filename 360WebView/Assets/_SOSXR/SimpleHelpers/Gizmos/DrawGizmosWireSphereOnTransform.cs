using UnityEngine;


public class DrawGizmosWireSphereOnTransform : DrawGizmosBase
{
    [SerializeField] private float m_radius = 0.05f;


    protected override void SetGizmoDetails()
    {
        Gizmos.DrawWireSphere(StartTransform.localPosition, m_radius * Vector3.one.x);
    }
}