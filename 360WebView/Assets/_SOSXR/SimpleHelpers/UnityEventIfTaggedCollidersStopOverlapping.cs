using SOSXR.EnhancedLogger;
using UnityEngine;
using UnityEngine.Events;


public class UnityEventIfTaggedCollidersStopOverlapping : MonoBehaviour
{
    [SerializeField] private UnityEvent m_eventToFire;
    [TagSelector] [SerializeField] private string m_tagToCheckAgainst = "MainCamera";
    private Collider _thisCollider;


    private void Awake()
    {
        _thisCollider = GetComponentInChildren<Collider>();
    }


    private void OnCollisionExit(Collision other)
    {
        if (!other.transform.CompareTag(m_tagToCheckAgainst))
        {
            return;
        }

        this.Debug(nameof(OnCollisionExit));
        FireEvent();
    }


    private void OnTriggerExit(Collider other)
    {
        /*if (!other.transform.CompareTag(m_tagToCheckAgainst))
        {
            return;
        }

        this.Debug(nameof(OnTriggerExit));
        FireEvent();*/
    }


    private void FireEvent()
    {
        m_eventToFire.Invoke();

        this.Debug("Exited tagged collider box, fired event");
    }
}