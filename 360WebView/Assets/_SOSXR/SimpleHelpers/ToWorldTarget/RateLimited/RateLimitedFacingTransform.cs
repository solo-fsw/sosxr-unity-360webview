using System.Collections;
using mrstruijk.Extensions;
using SOSXR.EnhancedLogger;
using UnityEngine;


namespace mrstruijk.SimpleHelpers
{
    /// <summary>
    ///     This uses LookAt position.
    ///     Can 0-out axis you don't want to use
    ///     Uses real world position (as opposed to localPosition)
    ///     It uses a Coroutine which runs every 'm_inverseRepeatRate' seconds
    /// </summary>
    public class RateLimitedFacingTransform : RateLimitedToWorldTargetBase
    {
        [Tooltip("0 to disable rotating over that Axis")]
        [SerializeField] private Vector3 m_zeroOutOrientations = new(0, 1, 0);


        protected override void FindTarget()
        {
            if (Target == null)
            {
                this.Warning("Target cannot be found");
            }
        }


        protected override IEnumerator ToTargetCR()
        {
            for (;;)
            {
                yield return new WaitForSeconds(m_inverseRepeatRate);
                var position = Target.position;
                var direction = position - transform.position;

                direction.ZeroOutVector3(m_zeroOutOrientations);

                transform.LookAt(position - direction);
            }
        }
    }
}