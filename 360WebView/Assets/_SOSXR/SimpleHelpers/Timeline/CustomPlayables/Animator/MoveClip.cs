using UnityEngine;


public class MoveClip : AnimatorClip
{
    public string xName = "X";
    public string yName = "Y";
    public string zName = "Z";
    public Vector3 movement;
    public bool reset = true;
    public float resetToValue;


    public override void FindIndexes(AnimatorBehaviour behaviour)
    {
        behaviour.xIndex = Animator.StringToHash(xName);
        behaviour.yIndex = Animator.StringToHash(yName);
        behaviour.zIndex = Animator.StringToHash(zName);
    }


    protected override void SetClipOnBehaviour(AnimatorBehaviour animatorBehaviour)
    {
        animatorBehaviour.animatorClip = this;
    }


    protected override void SetValuesOnBehaviourFromClip(AnimatorBehaviour behaviour)
    {
        behaviour.movement.x = movement.x;

        behaviour.movement.y = movement.y;

        behaviour.movement.z = movement.z;

        behaviour.reset = reset;
        behaviour.resetToValue = resetToValue;
    }


    protected override string SetDisplayName()
    {
        var dispName = "";

        if (movement.x != 0)
        {
            dispName += xName + CustomPlayableClipHelper.Colon + movement.x + CustomPlayableClipHelper.Divider;
        }

        if (movement.y != 0)
        {
            dispName += yName + CustomPlayableClipHelper.Colon + movement.y + CustomPlayableClipHelper.Divider;
        }

        if (movement.z != 0)
        {
            dispName += zName + CustomPlayableClipHelper.Colon + movement.z + CustomPlayableClipHelper.Divider;
        }

        return dispName;
    }
}