using System.Collections.Generic;
using mrstruijk.Extensions;
using SOSXR.EnhancedLogger;
using UnityEngine;


public class LerpBetweenTargets : MonoBehaviour
{
    [SerializeField] private Transform m_lerpTransform;

    [SerializeField] private List<Transform> m_targets;

    [SerializeField] private float m_speed = 1f;

    [SerializeField] private bool m_loop = true;

    private bool _allowLerp = false;

    private int _count = 0;


    private void Awake()
    {
        if (m_lerpTransform == null)
        {
            m_lerpTransform = transform;
            this.Info("No lerp transform set. Using own transform as lerp transform.");
        }
    }


    private void Update()
    {
        if (m_targets.Count == 0)
        {
            this.Warning("No targets set.");

            return;
        }

        if (!_allowLerp)
        {
            return;
        }

        m_lerpTransform.LerpToNewPosition(m_targets[_count], m_speed);

        this.Info("Lerping to", m_targets[_count].name);
    }


    public void SetLerpSpeed(float speed = 2f)
    {
        m_speed = speed;
    }


    public void NextTarget()
    {
        if (_count < m_targets.Count - 1)
        {
            _count++;
        }
        else if (m_loop)
        {
            _count = 0;
        }

        _allowLerp = true;
    }


    public void PreviousTarget()
    {
        if (_count > 0)
        {
            _count--;
        }
        else if (m_loop)
        {
            _count = m_targets.Count - 1;
        }

        _allowLerp = true;
    }


    public void SetTarget(int index)
    {
        if (index < m_targets.Count)
        {
            _count = index;
        }

        _allowLerp = true;
    }


    public void StopLerping()
    {
        _allowLerp = false;
    }
}