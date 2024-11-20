using System.Collections.Generic;
using UnityEngine;


public class ITLStopObjectCues : MonoBehaviour, ITLActivate
{
    [SerializeField] private List<ObjectCue> s_objectCues;


    public void TLActivate()
    {
        if (s_objectCues == null || s_objectCues.Count == 0)
        {
            return;
        }

        foreach (var objectCue in s_objectCues)
        {
            //   objectCue.StopCue();
        }
    }
}