using UnityEngine;
using UnityEngine.Playables;


public class TimelineSpeed : MonoBehaviour
{
    [Tooltip("Will try to get from this component if none set")]
    [SerializeField] private PlayableDirector director;

    [SerializeField] private double m_speed = 1;


    private void Awake()
    {
        if (director == null)
        {
            director = GetComponent<PlayableDirector>();
        }
    }


    public void SetSpeed()
    {
        if (director == null)
        {
            return;
        }

        ExecutiveProducer.SetTimelineSpeed(director, m_speed);
    }


    public void SetSpeed(double speed)
    {
        if (director == null)
        {
            return;
        }

        ExecutiveProducer.SetTimelineSpeed(director, speed);
    }
}