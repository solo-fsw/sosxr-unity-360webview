using mrstruijk.Extensions;
using UnityEngine;


namespace mrstruijk.SimpleHelpers
{
    /// <summary>
    ///     This uses LookAt position.
    ///     Can 0-out axis you don't want to use
    ///     Uses real world position (as opposed to localPosition)
    ///     It is set to LateUpdate, so  not for physics.
    /// </summary>
    public class ContinuousFacingTransform : ContinuousToWorldTargetBase
    {
        [Tooltip("0 to disable rotating over that Axis")]
        [SerializeField] private Vector3 m_zeroOutOrientations = new(0, 1, 0);


        protected override void ToTarget()
        {
            var position = m_target.position;
            var direction = position - transform.position;

            direction.ZeroOutVector3(m_zeroOutOrientations);

            transform.LookAt(position - direction);
        }
    }
}