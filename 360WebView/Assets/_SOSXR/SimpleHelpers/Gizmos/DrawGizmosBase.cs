using UnityEngine;


public abstract class DrawGizmosBase : MonoBehaviour
{
    [SerializeField] private bool m_onlyDrawInPlaymode;

    [Space(10)]
    [SerializeField] protected Transform m_startTransform;

    [SerializeField] private Color m_color = Color.blue;


    public Transform StartTransform
    {
        get => m_startTransform;
        set => m_startTransform = value;
    }


    private void Reset()
    {
        SetStartTransformIfNull();
        m_color = Random.ColorHSV();
    }


    protected virtual void Awake()
    {
        SetStartTransformIfNull();
    }


    /// <summary>
    ///     Is only called inside the Editor
    /// </summary>
    private void OnDrawGizmos()
    {
        if (m_onlyDrawInPlaymode && !Application.isPlaying)
        {
            return;
        }

        if (StartTransform == null)
        {
            return;
        }

        Gizmos.color = m_color;

        SetGizmoDetails();
    }


    private void SetStartTransformIfNull()
    {
        if (StartTransform == null)
        {
            StartTransform = transform; // If the StartTransform is null, use the transform of this object 
        }
    }


    protected abstract void SetGizmoDetails();
}