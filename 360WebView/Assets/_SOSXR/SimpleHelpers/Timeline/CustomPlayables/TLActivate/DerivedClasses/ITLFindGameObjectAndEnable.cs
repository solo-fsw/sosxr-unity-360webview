using mrstruijk;
using SOSXR.EnhancedLogger;
using UnityEngine;


public class ITLFindGameObjectAndEnable : MonoBehaviour, ITLActivate
{
    [SerializeField] [TagSelector] private string m_tagToFind = "Participant_Canvas_Handle_Parent";
    [SerializeField] [DisableEditing] private GameObject m_gameObject;


    public void TLActivate()
    {
        FindGameObject();

        if (m_gameObject == null)
        {
            return;
        }

        var enabler = m_gameObject.GetComponent<MassEnabler>();

        if (enabler == null)
        {
            this.Error("No MassEnabler found on " + m_gameObject.name + ".");

            return;
        }

        enabler.EnableAll();
    }


    private void FindGameObject()
    {
        if (m_gameObject != null)
        {
            return;
        }

        if (GameObject.FindGameObjectWithTag(m_tagToFind) == null)
        {
            this.Warning("No gameObject found with tag " + m_tagToFind + "in scene.");

            return;
        }

        m_gameObject = GameObject.FindGameObjectWithTag(m_tagToFind);
    }
}