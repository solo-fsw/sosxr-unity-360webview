using mrstruijk.Extensions;
using SOSXR.EnhancedLogger;
using UnityEngine;


public class SetAnimatorParameter : MonoBehaviour
{
    [SerializeField] private Animator m_animator;


    private void Awake()
    {
        if (m_animator == null)
        {
            m_animator = GetComponent<Animator>();
        }
    }


    public void SetAnimatorBoolOn(string parameter)
    {
        if (m_animator == null)
        {
            this.Error("No animator found");

            return;
        }

        if (!m_animator.HasParameter(parameter))
        {
            this.Warning("Animator does not have parameter", parameter);

            return;
        }

        m_animator.SetBool(parameter, true);
    }


    public void SetAnimatorBoolOff(string parameter)
    {
        if (m_animator == null)
        {
            this.Error("No animator found");

            return;
        }

        if (!m_animator.HasParameter(parameter))
        {
            this.Warning("Animator does not have parameter", parameter);

            return;
        }

        m_animator.SetBool(parameter, false);
    }


    public void SetAnimatorTrigger(string parameter)
    {
        if (m_animator == null)
        {
            this.Error("No animator found");

            return;
        }

        if (!m_animator.HasParameter(parameter))
        {
            this.Warning("Animator does not have parameter", parameter);

            return;
        }

        m_animator.SetTrigger(parameter);
    }
}