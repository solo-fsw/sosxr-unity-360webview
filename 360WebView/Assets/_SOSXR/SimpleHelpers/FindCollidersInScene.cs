using UnityEngine;


public class FindCollidersInScene : MonoBehaviour
{
    [SerializeField] protected Collider[] m_allColliders;


    protected virtual void FindAllColliders()
    {
        m_allColliders = FindObjectsOfType<Collider>();
    }


    private void DeleteAllColliders()
    {
        foreach (var coll in m_allColliders)
        {
            DestroyImmediate(coll);
        }
    }
}