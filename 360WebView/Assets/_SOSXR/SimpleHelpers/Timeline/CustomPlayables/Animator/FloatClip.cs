using UnityEngine;


public class FloatClip : AnimatorClip
{
    public string floatName = "";
    public float floatValue;
    public float minFloat = 0.02f;


    public override void FindIndexes(AnimatorBehaviour behaviour)
    {
        behaviour.floatIndex = Animator.StringToHash(floatName);
    }


    protected override void SetClipOnBehaviour(AnimatorBehaviour animatorBehaviour)
    {
        animatorBehaviour.animatorClip = this;
    }


    protected override void SetValuesOnBehaviourFromClip(AnimatorBehaviour behaviour)
    {
        behaviour.floatValue = floatValue;
    }


    protected override string SetDisplayName()
    {
        var dispName = "";

        if (floatName != "")
        {
            dispName += floatName + CustomPlayableClipHelper.Colon + floatValue;
        }

        return dispName;
    }
}