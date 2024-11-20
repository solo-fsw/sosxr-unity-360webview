using UnityEngine;


namespace mrstruijk.SimpleHelpers
{
    public class DestroyGameObject : MonoBehaviour
    {
        [SerializeField] private GameObject m_target;
        [SerializeField] private float m_delay = 0.25f;


        public void DestroyAfterDelay()
        {
            if (m_target == null)
            {
                m_target = gameObject;
            }

            Destroy(m_target, m_delay);
        }
    }
}