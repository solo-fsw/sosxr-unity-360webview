using UnityEngine;


public class IntClip : AnimatorClip
{
    public string integerName = "";
    public int integerValue;


    public override void FindIndexes(AnimatorBehaviour behaviour)
    {
        behaviour.integerIndex = Animator.StringToHash(integerName);
    }


    protected override void SetClipOnBehaviour(AnimatorBehaviour animatorBehaviour)
    {
        animatorBehaviour.animatorClip = this;
    }


    protected override void SetValuesOnBehaviourFromClip(AnimatorBehaviour behaviour)
    {
        behaviour.integerValue = integerValue;
    }


    protected override string SetDisplayName()
    {
        var dispName = "";

        if (integerName != "")
        {
            dispName += integerName + CustomPlayableClipHelper.Colon + integerValue;
        }

        return dispName;
    }
}