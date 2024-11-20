using mrstruijk.Events;
using SOSXR.EnhancedLogger;
using UnityEngine;
using UnityEngine.Events;


public class ChiralityUnityResponse : MonoBehaviour
{
    [Header("Events - Grabbed")]
    [SerializeField] private UnityEvent m_leftHandGrabbed;
    [SerializeField] private UnityEvent m_rightHandGrabbed;
    [SerializeField] private UnityEvent m_grabbedWithEitherHand;

    [Header("Events - Released")]
    [SerializeField] private UnityEvent m_leftHandReleased;
    [SerializeField] private UnityEvent m_rightHandReleased;
    [SerializeField] private UnityEvent m_releasedWithEitherHand;


    private void OnEnable()
    {
        EventsSystem.GrabbedWithHand += GrabbedWithHand;
        EventsSystem.ReleasedWithHand += ReleasedWithHand;
    }


    private void GrabbedWithHand(Chirality chirality)
    {
        if (chirality == Chirality.Left)
        {
            m_leftHandGrabbed?.Invoke();
        }
        else if (chirality == Chirality.Right)
        {
            m_rightHandGrabbed?.Invoke();
        }
        else
        {
            this.Warning("Invalid chirality value: " + chirality);
        }

        m_grabbedWithEitherHand?.Invoke();
        this.Info("Grabbed with", chirality, "sending UnityEvents");
    }


    private void ReleasedWithHand(Chirality chirality)
    {
        if (chirality == Chirality.Left)
        {
            m_leftHandReleased?.Invoke();
        }
        else if (chirality == Chirality.Right)
        {
            m_rightHandReleased?.Invoke();
        }
        else
        {
            this.Warning("Invalid chirality value: " + chirality);
        }

        m_releasedWithEitherHand?.Invoke();
        this.Info("Released with", chirality, "sending UnityEvents");
    }


    private void OnDisable()
    {
        EventsSystem.GrabbedWithHand -= GrabbedWithHand;
        EventsSystem.ReleasedWithHand -= ReleasedWithHand;
    }
}