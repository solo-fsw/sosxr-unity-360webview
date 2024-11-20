using System.Collections.Generic;
using mrstruijk;
using mrstruijk.Extensions;
using SOSXR.EnhancedLogger;
using UnityEngine;
using UnityEngine.Events;


public class ITLSendUnityEventFromList : MonoBehaviour, ITLActivate
{
    [SerializeField] private List<UnityEvent> m_unityEvents;
    [SerializeField] [DisableEditing] private int m_nextIndex;


    public void TLActivate()
    {
        if (m_unityEvents.ContainsIndex(m_nextIndex))
        {
            this.Debug("We will fire a UnityEvent with index", m_nextIndex, "from our list");

            m_unityEvents[m_nextIndex]?.Invoke();
            m_nextIndex++;
        }
        else
        {
            this.Warning("We're trying to fire an ITL Unity Event", m_nextIndex, "which does not exist in our list");
        }
    }
}