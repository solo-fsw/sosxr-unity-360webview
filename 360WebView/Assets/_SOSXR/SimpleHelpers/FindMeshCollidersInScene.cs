using UnityEngine;


public class FindMeshCollidersInScene : FindCollidersInScene
{
    protected override void FindAllColliders()
    {
        m_allColliders = FindObjectsOfType<MeshCollider>();
    }
}