using UnityEngine;
using UnityEngine.Events;


public class UnityEventListener : GameEventListenerBase
{
    [SerializeField] private UnityEvent m_unityEvent;


    public override void OnEventRaised()
    {
        m_unityEvent?.Invoke();
    }
}