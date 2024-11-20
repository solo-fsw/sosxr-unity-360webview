using UnityEngine;
using UnityEngine.Timeline;


public class TriggerClip : AnimatorClip
{
    public string triggerName = "";
    public bool forceTriggerClipLength = true;

    [ReadOnly] public string animationClipName;
    [ReadOnly] public float animationDuration;
    private int _triggerIndex;


    public override void FindIndexes(AnimatorBehaviour behaviour)
    {
        _triggerIndex = Animator.StringToHash(triggerName);
        behaviour.triggerIndex = _triggerIndex;
    }


    protected override void SetClipOnBehaviour(AnimatorBehaviour animatorBehaviour)
    {
        animatorBehaviour.animatorClip = this;
    }


    /// <summary>
    ///     Here we set the clip duration to the length that's set by the values on the clip itself.
    /// </summary>
    /// <param name="clip"></param>
    protected override void SetClipDuration(TimelineClip clip)
    {
        if (clip == null)
        {
            return;
        }

        if (forceTriggerClipLength && animationDuration != 0)
        {
            clip.duration = animationDuration;
        }
    }


    protected override void SetValuesOnBehaviourFromClip(AnimatorBehaviour animatorBehaviour)
    {
        // Nothing
    }


    protected override string SetDisplayName()
    {
        var displayName = "";

        if (triggerName != "")
        {
            displayName += triggerName;
        }

        return displayName;
    }
}