using System.Collections;
using GPInteraction._Scripts;
using UnityEngine;


namespace mrstruijk.SimpleHelpers
{
    public class RateLimitedCopyFromSOAssets : RateLimitedToWorldTargetBase
    {
        [SerializeField] private SOVector3Asset m_targetPosition;
        [SerializeField] private SOQuaternionAsset m_targetRotation;


        protected override IEnumerator ToTargetCR()
        {
            if (m_targetPosition == null || m_targetRotation == null)
            {
                yield break;
            }

            for (;;)
            {
                yield return new WaitForSeconds(m_inverseRepeatRate);
                var trans = transform;

                trans.position = m_targetPosition.Value;
                trans.rotation = m_targetRotation.Value;
            }
        }
    }
}