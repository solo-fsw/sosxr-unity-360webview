using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;


public class DirectorMasterController : MonoBehaviour
{
    private List<PlayableDirector> _directors = new();
    private List<TimelineControl> _tlControllers = new();

    public bool OneDirectorIsPlaying => _directors != null && CheckAtLeastOneDirectorIsPlaying();


    private bool CheckAtLeastOneDirectorIsPlaying()
    {
        return _directors.Any(director => director.playableGraph.IsValid() && director.playableGraph.IsPlaying());
    }


    private void Awake()
    {
        _directors = FindObjectsOfType<PlayableDirector>().ToList();
        _tlControllers = FindObjectsOfType<TimelineControl>().ToList();
    }


    private void OnEnable()
    {
        if (_tlControllers == null)
        {
            return;
        }

        foreach (var timelineControl in _tlControllers)
        {
            timelineControl.AddDirectorMasterController(this);
        }
    }


    private void OnDisable()
    {
        if (_tlControllers == null)
        {
            return;
        }

        foreach (var timelineControl in _tlControllers)
        {
            timelineControl.RemoveDirectorMasterController(this);
        }
    }
}