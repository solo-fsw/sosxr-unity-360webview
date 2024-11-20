using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;


[Serializable]
public class FadeSettings
{
    [FormerlySerializedAs("isEnabled")] public bool IsEnabled = true;
    [FormerlySerializedAs("profilerTag")] public string ProfilerTag = "Screen Fade";

    [FormerlySerializedAs("renderPassEvent")]
    public RenderPassEvent RenderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
    [FormerlySerializedAs("material")] public Material Material;
    [NonSerialized] public Material RunTimeMaterial = null;


    public bool AreValid()
    {
        return RunTimeMaterial != null && IsEnabled;
    }
}