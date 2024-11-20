using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class ScreenFadePass : ScriptableRenderPass
{
    private readonly FadeSettings _fadeSettings;


    public ScreenFadePass(FadeSettings newSettings)
    {
        _fadeSettings = newSettings;
        renderPassEvent = newSettings.RenderPassEvent;
    }


    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        var command = CommandBufferPool.Get(_fadeSettings.ProfilerTag);
        RenderTargetIdentifier source = BuiltinRenderTextureType.CameraTarget;
        RenderTargetIdentifier destination = BuiltinRenderTextureType.CurrentActive;

        command.Blit(source, destination, _fadeSettings.RunTimeMaterial);
        context.ExecuteCommandBuffer(command);

        CommandBufferPool.Release(command);
    }
}