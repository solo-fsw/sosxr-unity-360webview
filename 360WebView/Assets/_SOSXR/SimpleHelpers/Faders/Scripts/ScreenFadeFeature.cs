using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;


public class ScreenFadeFeature : ScriptableRendererFeature
{
    [FormerlySerializedAs("Settings")] [FormerlySerializedAs("settings")] public FadeSettings FadeSettings;
    private ScreenFadePass _renderPass;


    public override void Create()
    {
        _renderPass = new ScreenFadePass(FadeSettings);
    }


    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (FadeSettings.AreValid())
        {
            renderer.EnqueuePass(_renderPass);
        }
    }
}