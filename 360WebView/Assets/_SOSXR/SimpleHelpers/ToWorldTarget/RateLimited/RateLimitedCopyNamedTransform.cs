using System.Collections;
using mrstruijk.Extensions;
using SOSXR.EnhancedLogger;
using UnityEngine;


namespace mrstruijk.SimpleHelpers
{
    public class RateLimitedCopyNamedTransform : RateLimitedToWorldTargetBase
    {
        [Tooltip("Start searching from this parent Transform to the TargetName mentioned below. \n" +
                 "If this Parent is not set, it is assumed that current Transform is the desired Parent.")]
        [SerializeField] private Transform m_parent;
        [SerializeField] private string m_targetName;


        protected override void FindTarget()
        {
            if (m_parent == null)
            {
                m_parent = transform;
            }

            if (Target != null)
            {
                return;
            }

            //if (m_parent.FindSubChildTransformInParent(m_targetName).transform != null)
            {
                Target = m_parent.FindChildByName(m_targetName);
            }


            if (Target == null)
            {
                this.Error("Cannot find the correct Named Target");
            }
        }


        protected override IEnumerator ToTargetCR()
        {
            for (;;)
            {
                yield return new WaitForSeconds(m_inverseRepeatRate);

                SetPositionAndRotation();
            }
        }


        private void SetPositionAndRotation()
        {
            transform.SetPositionAndRotation(Target.position, Target.rotation);
        }
    }
}