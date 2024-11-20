using System.Collections.Generic;
using mrstruijk;
using SOSXR.EnhancedLogger;
using UnityEngine;


[CreateAssetMenu(menuName = "mrstruijk/EventsSystems/GameEvent")]
public class GameEventSO : ScriptableObject
{
    [DisableEditing] [SerializeField] private List<GameEventListenerBase> m_listeners = new();


    public void Raise()
    {
        for (var i = m_listeners.Count - 1; i >= 0; i--)
        {
            m_listeners[i].OnEventRaised();
        }

        this.Success("Raised Event", name, " on", m_listeners.Count, "listeners");
    }


    public void RegisterListener(GameEventListenerBase listener)
    {
        m_listeners.Add(listener);
    }


    public void UnregisterListener(GameEventListenerBase listener)
    {
        m_listeners.Remove(listener);
    }
}