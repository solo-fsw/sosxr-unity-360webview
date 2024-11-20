// using DG.Tweening;

using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;


public class ScreenFader : MonoBehaviour
{
    [FormerlySerializedAs("rendererData")] public UniversalRendererData RendererData;

    [Range(0, 5)] public float DefaultDuration = 1f;

    private Material _fadeMaterial;

    private const string Alpha = "_Alpha";


    private void Awake()
    {
        SetupFadeFeature();
    }


    private void SetupFadeFeature()
    {
        var feature = RendererData.rendererFeatures.Find(item => item is ScreenFadeFeature);

        if (feature is not ScreenFadeFeature screenFade)
        {
            return;
        }

        _fadeMaterial = Instantiate(screenFade.FadeSettings.Material);
        screenFade.FadeSettings.RunTimeMaterial = _fadeMaterial;
    }


    public float FadeToBlack()
    {
        // _fadeMaterial.DOFloat(1f, Alpha, DefaultDuration);

        return DefaultDuration;
    }


    public float FadeToBlack(float duration)
    {
        // _fadeMaterial.DOFloat(1f, Alpha, duration);

        return duration;
    }


    public float FadeToClear()
    {
        // _fadeMaterial.DOFloat(0f, Alpha, DefaultDuration);

        return DefaultDuration;
    }


    public float FadeToClear(float duration)
    {
        // _fadeMaterial.DOFloat(0f, Alpha, duration);

        return duration;
    }
}