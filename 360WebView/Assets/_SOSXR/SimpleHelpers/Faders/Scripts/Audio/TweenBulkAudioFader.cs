using System.Collections.Generic;
using UnityEngine;


// using DG.Tweening;


public class TweenBulkAudioFader : MonoBehaviour
{
    [Tooltip("These ones will be excluded from fading")]
    public List<AudioSource> ContinuousAudioSources;


    public void StartAudioFade(float fadeDuration)
    {
        var audioSources = FindObjectsOfType<AudioSource>();

        foreach (var source in audioSources)
        {
            if (ContinuousAudioSources.Contains(source))
            {
            }

            // source.DOFade(0, fadeDuration);
        }
    }
}