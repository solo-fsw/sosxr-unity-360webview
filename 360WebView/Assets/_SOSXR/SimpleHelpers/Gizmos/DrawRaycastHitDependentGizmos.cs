using mrstruijk.SimpleHelpers;
using SOSXR.EnhancedLogger;
using UnityEngine;


public class DrawRaycastHitDependentGizmos : DrawGizmosBase
{
    [SerializeField] private Color m_hitColor = Color.blue;
    [SerializeField] private Color m_missColor = Color.red;
    [SerializeField] private ToggleGameObjectViaRayCast m_toggleGameObjectViaRayCast;


    protected override void SetGizmoDetails()
    {
        if (m_toggleGameObjectViaRayCast == null)
        {
            this.Error("Cannot draw gizmos, since component has not been found");

            return;
        }

        if (m_toggleGameObjectViaRayCast.Hit.transform == null)
        {
            DrawNoHitGizmo(m_startTransform.position, -m_startTransform.forward);
        }
        else
        {
            DrawHitGizmo(m_startTransform.position, -m_startTransform.forward);
        }
    }


    private void DrawNoHitGizmo(Vector3 thisPosition, Vector3 thisForward)
    {
        Gizmos.color = m_missColor;
        Gizmos.DrawRay(thisPosition, thisForward * m_toggleGameObjectViaRayCast.RaycastDistance);
    }


    private void DrawHitGizmo(Vector3 thisPosition, Vector3 thisForward)
    {
        Gizmos.color = m_hitColor;
        Gizmos.DrawRay(thisPosition, thisForward * m_toggleGameObjectViaRayCast.Hit.distance);
    }
}