using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public class UnityEventOnCollisionEnter : MonoBehaviour
{
    [SerializeField] private UnityEvent m_eventToFire;
    [SerializeField] private Collider[] m_excludeColliders;


    private void OnCollisionEnter(Collision other)
    {
        if (m_excludeColliders.Contains(other.collider))
        {
            return;
        }

        m_eventToFire.Invoke();
    }
}