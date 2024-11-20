using mrstruijk.Events;
using SOSXR.EnhancedLogger;
using UnityEngine;
using UnityEngine.Playables;


public class TimelineControl : MonoBehaviour
{
    [Tooltip("Will find on this gameobject if null (required anyway)")]
    //[SerializeField] private DirectorPublishingEvents m_directorPublishing;
    [SerializeField] private PlayableDirector m_director;
    [SerializeField] private bool m_setSpeed;
    [SerializeField] private bool m_playTimeline = true;

    [Tooltip("How often is the referenced Timeline allowed to play using Gaze \n-1 is infinite")]
    [SerializeField] [Range(-1, 10)] private int m_allowedPlays = 1;

    private int _countedPlays;

    private DirectorMasterController _masterController;

    private bool IsPlaying => m_director.playableGraph.IsValid() && m_director.playableGraph.IsPlaying();

    private bool AllowedToPlay
    {
        get
        {
            if (m_allowedPlays == -1)
            {
                return true;
            }

            return _countedPlays < m_allowedPlays;
        }
    }


    private double TimelineSpeed => m_director.playableGraph.GetRootPlayable(0).GetSpeed();


    private void Awake()
    {
        /*if (m_directorPublishing != null)
        {
            return;
        }

        m_directorPublishing = GetComponent<DirectorPublishingEvents>();*/
    }


    private void OnEnable()
    {
        m_director.stopped += context => IncrementCountedPlays();
        EventsSystem.GazedLongEnough += Respond;
    }


    public void AddDirectorMasterController(DirectorMasterController directorMaster)
    {
        _masterController = directorMaster;
    }


    private void Respond()
    {
        if (IsPlaying)
        {
            SetSpeed();

            return;
        }

        PlayTimeline();
    }


    private void SetSpeed()
    {
        if (m_setSpeed == false)
        {
            return;
        }

        if (TimelineSpeed == 0)
        {
            ExecutiveProducer.SetTimelineSpeed(m_director, 1);
        }
    }


    private void PlayTimeline()
    {
        if (m_playTimeline == false)
        {
            return;
        }

        if (_masterController.OneDirectorIsPlaying)
        {
            return;
        }

        if (AllowedToPlay == false)
        {
            return;
        }

        // m_directorPublishing.Director = m_director;
        // m_directorPublishing.EventCode = NetworkEventCode.DirectorPlay;
        // m_directorPublishing.RaisePhotonEvent();

        this.Error("This is not actyally calling anything atm");
    }


    private void IncrementCountedPlays()
    {
        _countedPlays++;
    }


    public void RemoveDirectorMasterController(DirectorMasterController directorMaster)
    {
        if (_masterController == directorMaster)
        {
            _masterController = null;
        }
    }


    private void OnDisable()
    {
        m_director.stopped -= context => IncrementCountedPlays();
        EventsSystem.GazedLongEnough -= Respond;
    }
}