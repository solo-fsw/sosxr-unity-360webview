using mrstruijk.Events;
using UnityEngine;
using UnityEngine.Events;


public class RequestJoinCodeKeyboardUnityEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent m_eventToFire;


    private void OnEnable()
    {
        EventsSystem.RequestJoinCodeKeyboard += m_eventToFire.Invoke;
    }


    private void OnDisable()
    {
        EventsSystem.RequestJoinCodeKeyboard -= m_eventToFire.Invoke;
    }
}