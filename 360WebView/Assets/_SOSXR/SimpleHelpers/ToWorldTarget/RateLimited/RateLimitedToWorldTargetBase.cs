using System.Collections;
using UnityEngine;


namespace mrstruijk.SimpleHelpers
{
    public abstract class RateLimitedToWorldTargetBase : MonoBehaviour
    {
        [SerializeField] private bool m_requiresTarget = true;
        [SerializeField] [Range(0.001f, 1f)] protected float m_inverseRepeatRate = 0.1f;
        [SerializeField] protected Transform Target;

        private Coroutine _coroutine;

        public float InverseRepeatRate
        {
            get => m_inverseRepeatRate;
            set => m_inverseRepeatRate = value;
        }


        private void Update()
        {
            if (m_requiresTarget && Target == null)
            {
                FindTarget();
            }

            if (Target == null)
            {
                return;
            }

            _coroutine ??= StartCoroutine(ToTargetCR());
        }


        protected virtual void FindTarget()
        {
        }


        protected abstract IEnumerator ToTargetCR();


        private void OnDisable()
        {
            StopAllCoroutines();
            _coroutine = null;
        }
    }
}