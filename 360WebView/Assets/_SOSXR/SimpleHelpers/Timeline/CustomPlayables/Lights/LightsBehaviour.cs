using System;
using UnityEngine;
using UnityEngine.Playables;


/// <summary>
///     This acts as our data for the clip to write to
///     Adapted from GameDevGuide: https://youtu.be/12bfRIvqLW4
/// </summary>
[Serializable]
public class LightsBehaviour : PlayableBehaviour
{
    public Color32 Color = new(1, 1, 1, 1);
    public float Intensity;
    public float Range;
    public string ClipName { get; set; }
    public object TrackBinding { get; set; }
}