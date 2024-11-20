using UnityEngine;
using UnityEngine.Playables;


/// <summary>
///     Adapted from GameDevGuide: https://youtu.be/12bfRIvqLW4
/// </summary>
public class LightsTrackMixer : PlayableBehaviour
{
    private int _previousIndex = -1;


    /// <summary>
    ///     Tell playable what to do when the playhead is on this clip
    /// </summary>
    /// <param name="playable"></param>
    /// <param name="info"></param>
    /// <param name="playerData"></param>
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var
            data = (Light) playerData; // The playerData is the object that our track is bound to, so cast to the binding of the Track

        if (!data)
        {
            return;
        }

        SetLightFromCurrentClipValue(playable, data);
    }


    private void SetLightFromCurrentClipValue(Playable playable, Light data)
    {
        var inputCount = playable.GetInputCount(); // Get all clips on our track

        for (var i = 0; i < inputCount; i++)
        {
            var inputWeight = playable.GetInputWeight(i); // get inputWeight for our current index

            if (inputWeight <= 0.0001f)
            {
                continue;
            }

            var inputPlayable = (ScriptPlayable<LightsBehaviour>) playable.GetInput(i); // Use this as our active clip
            var input = inputPlayable.GetBehaviour();

            data.intensity = input.Intensity * inputWeight;
            data.range = input.Range * inputWeight;

            if (i == _previousIndex)
            {
                continue;
            }

            data.color = input.Color;
            _previousIndex = i;
        }
    }
}