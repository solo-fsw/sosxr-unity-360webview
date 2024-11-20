using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;


[Serializable]
public class AnimatorBehaviour : PlayableBehaviour
{
    public AnimatorClip animatorClip;
    public Animator trackBinding;

    public int xIndex;
    public int yIndex;
    public int zIndex;
    public Vector3 movement;

    public int floatIndex;
    public float floatValue;

    public bool reset;
    public float resetToValue;

    public int integerIndex;
    public int integerValue;

    public int triggerIndex;

    public bool boolValue = true;
    public bool resetBool = true;
    public int boolIndex;
    private bool _booled;
    private bool _found;
    private bool _priorBoolValue;
    private bool _triggered;


    public override void OnGraphStart(Playable playable)
    {
        FindIndexesOnClips();
    }


    private void FindIndexesOnClips()
    {
        if (animatorClip == null)
        {
            return;
        }

        animatorClip.FindIndexes(this);
    }


    /// <summary>
    ///     Tell playable what to do when the playhead is on this clip
    /// </summary>
    /// <param name="playable"></param>
    /// <param name="info"></param>
    /// <param name="playerData"></param>
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (trackBinding == null)
        {
            trackBinding =
                (Animator) playerData; // The playerData is the object that our track is bound to, so cast to the binding of the Track
        }

        if (trackBinding == null)
        {
            return;
        }

        if (!Application.isPlaying)
        {
            return;
        }

        DoMove(info.weight);

        DoFloat(info.weight);

        DoInt(info.weight);

        DoTrigger();

        DoBool();
    }


    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (trackBinding == null)
        {
            return;
        }

        if (!Application.isPlaying)
        {
            return;
        }

        ResetFloatNoClipsPlaying();
        ResetBool();

        _triggered = false;
        _booled = false;
    }


    private void ResetFloatNoClipsPlaying()
    {
        if (reset == false)
        {
            return;
        }

        if (xIndex != 0)
        {
            trackBinding.SetFloat(xIndex, resetToValue);
        }

        if (yIndex != 0)
        {
            trackBinding.SetFloat(yIndex, resetToValue);
        }

        if (zIndex != 0)
        {
            trackBinding.SetFloat(zIndex, resetToValue);
        }

        if (floatIndex != 0)
        {
            trackBinding.SetFloat(floatIndex, resetToValue);
        }
    }


    private void DoMove(float inputWeight)
    {
        if (xIndex != 0 && movement.x != 0f)
        {
            trackBinding.SetFloat(xIndex, movement.x * inputWeight);
        }

        if (yIndex != 0 && movement.y != 0f)
        {
            trackBinding.SetFloat(yIndex, movement.y * inputWeight);
        }

        if (zIndex != 0 && movement.z != 0f)
        {
            trackBinding.SetFloat(zIndex, movement.z * inputWeight);
        }
    }


    private void DoFloat(float inputWeight)
    {
        if (floatIndex != 0 && floatValue != 0f)
        {
            trackBinding.SetFloat(floatIndex, floatValue * inputWeight);
        }
    }


    private void DoInt(float inputWeight)
    {
        if (integerIndex != 0 && integerValue != 0)
        {
            trackBinding.SetInteger(integerIndex,
                Mathf.RoundToInt(integerValue *
                                 inputWeight)); // Test the rounding in your project! If problems with rounding, replace with trackBinding.SetInteger(integerIndex, integerValue);
        }
    }


    private async void DoTrigger()
    {
        if (triggerIndex == 0)
        {
            return;
        }

        if (_triggered == false)
        {
            trackBinding.SetTrigger(triggerIndex);
            _triggered = true;
        }

        await GetNextClipInfo();
    }


    private async Task GetNextClipInfo()
    {
        var end = Time.time + 1f;

        while (Time.time < end)
        {
            await Task.Yield();
        }

        if (_found == false)
        {
            var clip = trackBinding.GetCurrentAnimatorClipInfo(0);

            var triggerClip = (TriggerClip) animatorClip;
            triggerClip.animationDuration = clip[0].clip.length;
            triggerClip.animationClipName = clip[0].clip.name;

            _found = true;
        }
    }


    private void DoBool()
    {
        if (boolIndex == 0)
        {
            return;
        }

        if (_booled == false)
        {
            if (resetBool)
            {
                _priorBoolValue = trackBinding.GetBool(boolIndex);
            }

            trackBinding.SetBool(boolIndex, boolValue);
            _booled = true;
        }
    }


    private void ResetBool()
    {
        if (boolIndex == 0)
        {
            return;
        }

        if (resetBool == false)
        {
            return;
        }

        trackBinding.SetBool(boolIndex, _priorBoolValue);
    }
}