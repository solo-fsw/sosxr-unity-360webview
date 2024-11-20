using System.Collections;
using SOSXR.EnhancedLogger;
using UnityEngine;


namespace mrstruijk.SimpleHelpers
{
    public class RateLimitedCopyTaggedTransform : RateLimitedToWorldTargetBase
    {
        [SerializeField] private string m_targetTag = "MainCamera";


        protected override void FindTarget()
        {
            if (Target != null)
            {
                return;
            }

            if (GameObject.FindWithTag(m_targetTag) != null)
            {
                Target = GameObject.FindWithTag(m_targetTag).transform;
            }

            if (Target == null)
            {
                this.Error("Cannot find the correct Tagged Target");
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