using SOSXR.EnhancedLogger;
using UnityEngine;
using UnityEngine.Events;


public class ITLUnityEvent : MonoBehaviour, ITLActivate
{
    [SerializeField] private UnityEvent m_eventToFire;


    public void TLActivate()
    {
        FireEvent();
    }


    public void FireEvent()
    {
        m_eventToFire?.Invoke();
        this.Success("Fired event via ITLUnityEvent");
    }
}