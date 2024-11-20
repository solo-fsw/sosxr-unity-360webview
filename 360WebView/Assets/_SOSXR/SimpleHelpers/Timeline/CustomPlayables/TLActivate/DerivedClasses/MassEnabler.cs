using System.Collections.Generic;
using UnityEngine;


public class MassEnabler : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_gameObjectsToEnable;


    public void EnableAll()
    {
        foreach (var go in m_gameObjectsToEnable)
        {
            go.SetActive(true);
        }
    }
}