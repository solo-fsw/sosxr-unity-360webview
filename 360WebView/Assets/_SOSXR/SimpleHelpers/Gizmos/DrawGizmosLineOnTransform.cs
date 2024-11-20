using System;
using UnityEngine;


public class DrawGizmosLineOnTransform : DrawGizmosBase
{
    [SerializeField] private TransformDirections m_direction = TransformDirections.Forward;
    [SerializeField] private float m_lineLength = 0.25f;


    protected override void SetGizmoDetails()
    {
        var position = m_startTransform.position;

        switch (m_direction)
        {
            case TransformDirections.Forward:
                Gizmos.DrawLine(position, position + m_startTransform.forward * m_lineLength);

                break;
            case TransformDirections.Back:
                Gizmos.DrawLine(position, position - m_startTransform.forward * m_lineLength);

                break;
            case TransformDirections.Up:
                Gizmos.DrawLine(position, position + m_startTransform.up * m_lineLength);

                break;
            case TransformDirections.Down:
                Gizmos.DrawLine(position, position - m_startTransform.up * m_lineLength);

                break;
            case TransformDirections.Right:
                Gizmos.DrawLine(position, position + m_startTransform.right * m_lineLength);

                break;
            case TransformDirections.Left:
                Gizmos.DrawLine(position, position - m_startTransform.right * m_lineLength);

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}