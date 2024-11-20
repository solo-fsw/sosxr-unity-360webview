using SOSXR.EnhancedLogger;
using UnityEngine;
using UnityEngine.Serialization;


namespace mrstruijk.SimpleHelpers
{
    public class ContinuousLookAtTaggedTransform : ContinuousToWorldTargetBase
    {
        [FormerlySerializedAs("TargetTag")] [SerializeField] private string m_targetTag = "MainCamera";


        protected override void ToTarget()
        {
            transform.LookAt(m_target);
        }


        protected override void FindTarget()
        {
            if (m_target == null)
            {
                m_target = GameObject.FindWithTag(m_targetTag).transform;
            }

            ;

            if (m_target != null)
            {
                return;
            }

            this.Error("Cannot find the correct Tagged Target");
        }
    }
}