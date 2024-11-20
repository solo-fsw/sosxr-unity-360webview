using System.Linq;
using JetBrains.Annotations;
using SOSXR.EnhancedLogger;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Collider))]
public class UnityEventIfOnTriggerEnter : MonoBehaviour
{
    [Header("Tag")]
    [SerializeField] private bool m_checkSpecificTag = true;
    [TagSelector] [SerializeField] private string[] m_tagToCheckAgainst = {"FaceChooser"};

    [Header("Events")]
    [SerializeField] [CanBeNull] private UnityEvent<Collider> m_eventToFire;


    private void OnTriggerEnter(Collider other)
    {
        if (m_checkSpecificTag && !m_tagToCheckAgainst.Contains(other.tag))
        {
            return;
        }

        FireEvent(other);

        this.Success("Triggered UnityEvent on", gameObject.name);
    }


    private void FireEvent(Collider other = null)
    {
        m_eventToFire?.Invoke(other);
    }
}