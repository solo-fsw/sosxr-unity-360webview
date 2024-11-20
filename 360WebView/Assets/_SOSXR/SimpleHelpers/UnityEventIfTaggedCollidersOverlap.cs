using SOSXR.EnhancedLogger;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Collider))]
public class UnityEventIfTaggedCollidersOverlap : MonoBehaviour
{
    [SerializeField] private UnityEvent m_eventToFire;
    [TagSelector] [SerializeField] private string m_tagToCheckAgainst = "MainCamera";
    private Collider _thisCollider;


    private void Awake()
    {
        _thisCollider = GetComponent<Collider>();
    }


    public void CheckIfCollidersOverlap()
    {
        var taggedGameObject = GameObject.FindGameObjectWithTag(m_tagToCheckAgainst);

        if (taggedGameObject == null)
        {
            this.Error("No GameObject with tag " + m_tagToCheckAgainst + " found");

            return;
        }

        var taggedCollider = taggedGameObject.GetComponent<Collider>();

        if (_thisCollider.bounds.Intersects(taggedCollider.bounds))
        {
            FireEvent();
        }
    }


    private void FireEvent()
    {
        m_eventToFire.Invoke();

        this.Debug("Bounds Intersected, fired event");
    }
}