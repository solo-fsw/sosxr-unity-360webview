using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


/// <summary>
///     Adapted from GameDevGuide: https://youtu.be/12bfRIvqLW4
/// </summary>
public class LUTTrackMixer : PlayableBehaviour
{
    private ColorLookup _colorLookup;
    private float _currentContribution;
    private Texture _currentLutTexture;
    private int _previousIndex = -1;

    private bool _valueHasBeenSet;


    /// <summary>
    ///     Tell playable what to do when the playhead is on this clip
    /// </summary>
    /// <param name="playable"></param>
    /// <param name="info"></param>
    /// <param name="playerData"></param>
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var
            data = (VolumeProfile) playerData; // The playerData is the object that our track is bound to, so cast to the binding of the Track

        if (!data)
        {
            return;
        }

        if (_colorLookup == null)
        {
            data.TryGet(out _colorLookup);
        }

        if (Application.isPlaying)
        {
            GetCurrentClipValue(playable);
        }
    }


    private void GetCurrentClipValue(Playable playable)
    {
        var inputCount = playable.GetInputCount(); // Get all clips on our track

        for (var i = 0; i < inputCount; i++)
        {
            var inputWeight = playable.GetInputWeight(i); // get inputWeight for our current index

            if (!(inputWeight > 0f))
            {
                continue;
            }

            var inputPlayable = (ScriptPlayable<LUTBehaviour>) playable.GetInput(i); // Use this as our active clip
            var input = inputPlayable.GetBehaviour();

            if (i != _previousIndex)
            {
                _colorLookup.texture.value = input.LUTTexture;
                _previousIndex = i;
            }

            _colorLookup.contribution.value =
                input.Contribution * inputWeight; // Multiply values by inputWeight to allow easing in and out

            return;
        }
    }
}