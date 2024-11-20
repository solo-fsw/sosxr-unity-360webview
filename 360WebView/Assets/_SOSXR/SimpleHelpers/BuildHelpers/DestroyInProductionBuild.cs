using UnityEngine;


public class DestroyInProductionBuild : MonoBehaviour
{
    [SerializeField] private GameObject m_objectToDestroy;


    private void Awake()
    {
        if (m_objectToDestroy == null)
        {
            m_objectToDestroy = gameObject;
        }

        #if UNITY_EDITOR || DEVELOPMENT_BUILD
        // Do nothing
        #else
        DestroyComponents();
        #endif
    }


    private void DestroyComponents()
    {
        Destroy(m_objectToDestroy);
    }
}