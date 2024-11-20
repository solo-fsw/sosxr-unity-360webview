using SOSXR.EnhancedLogger;
using UnityEngine;
using UnityEngine.Events;


public class UnityEventAfterSeconds : MonoBehaviour
{
    [SerializeField] private bool m_startOnStart = true;
    [SerializeField] [Range(0f, 60f)] private float m_secondsToWaitAfterCalling = 5f;

    [Space(10)]
    [SerializeField] private UnityEvent m_eventToFire;


    private void Start()
    {
        if (m_startOnStart)
        {
            FireEventAfterDelay();
        }
    }


    public void FireEventAfterDelay()
    {
        Invoke(nameof(FireEvent), m_secondsToWaitAfterCalling);

        this.Info("Will fire event after", m_secondsToWaitAfterCalling, "seconds");
    }


    private void FireEvent()
    {
        m_eventToFire?.Invoke();

        this.Info("Event fired!");
    }
}