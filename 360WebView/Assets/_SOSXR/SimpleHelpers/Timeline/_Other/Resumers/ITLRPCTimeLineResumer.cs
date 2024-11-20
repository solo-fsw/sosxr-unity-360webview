using UnityEngine;


[RequireComponent(typeof(TimelineResumer))]
public class ITLRPCTimeLineResumer : MonoBehaviour, ITLActivate
{
    [SerializeField] private TimelineResumer m_timelineResumer;


    public void TLActivate()
    {
        Resume();
    }


    private void Awake()
    {
        if (m_timelineResumer == null)
        {
            m_timelineResumer = GetComponent<TimelineResumer>();
        }
    }


    public void Resume()
    {
        m_timelineResumer.ResumeTimeline();
    }
}