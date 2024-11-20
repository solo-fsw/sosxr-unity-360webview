using mrstruijk;
using SOSXR.EnhancedLogger;
using UnityEngine;


public class FindSetParameter : MonoBehaviour
{
    [SerializeField] [TagSelector] private string m_faxTag = "Fax";
    [SerializeField] [DisableEditing] private GameObject m_facsimile;
    [SerializeField] [DisableEditing] private SetAnimatorParameter m_setAnimatorParameter;


    private bool FindFax()
    {
        if (m_setAnimatorParameter != null)
        {
            this.Info("Fax, with correct component, already found");

            return true;
        }

        if (GameObject.FindGameObjectWithTag(m_faxTag) == null)
        {
            this.Warning("No gameobject with tag", m_faxTag, "found");

            return false;
        }

        m_facsimile = GameObject.FindGameObjectWithTag(m_faxTag);
        this.Info("Fax found");

        if (m_facsimile.GetComponent<SetAnimatorParameter>() == null)
        {
            m_setAnimatorParameter = m_facsimile.AddComponent<SetAnimatorParameter>();

            this.Warning("No SetAnimatorParameter found on", m_facsimile,
                "gameobject. I will add this right now, but that shouldn't happen this way. Mayday");
        }

        if (m_facsimile.GetComponent<SetAnimatorParameter>() != null)
        {
            m_setAnimatorParameter = m_facsimile.GetComponent<SetAnimatorParameter>();
        }

        return m_facsimile != null;
    }


    private void OnEnable()
    {
        // FindFax();
    }


    public void SetAnimatorTrigger(string parameter)
    {
        if (!FindFax())
        {
            this.Error("Cannot set Animator Trigger, Fax not found");

            return;
        }

        m_setAnimatorParameter.SetAnimatorTrigger(parameter);
    }
}