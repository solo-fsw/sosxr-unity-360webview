using System;
using UnityEngine;


[Serializable]
public class BoolClip : AnimatorClip
{
    public string boolName = "";
    public bool boolValue = true;
    public bool resetBool = true;
    private int _boolIndex;


    public override void FindIndexes(AnimatorBehaviour behaviour)
    {
        _boolIndex = Animator.StringToHash(boolName);
        behaviour.boolIndex = _boolIndex;
    }


    protected override void SetClipOnBehaviour(AnimatorBehaviour animatorBehaviour)
    {
        animatorBehaviour.animatorClip = this;
    }


    protected override void SetValuesOnBehaviourFromClip(AnimatorBehaviour behaviour)
    {
        behaviour.boolValue = boolValue;
        behaviour.resetBool = resetBool;
    }


    protected override string SetDisplayName()
    {
        var displayName = "";

        if (boolName != "")
        {
            displayName += boolName + CustomPlayableClipHelper.Colon + template.boolValue;
        }

        if (template.resetBool)
        {
            displayName += " [RESET]";
        }

        return displayName;
    }
}