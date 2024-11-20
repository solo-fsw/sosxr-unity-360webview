using UnityEngine;
using UnityEngine.Events;


public class UnityEventIfInBuild : MonoBehaviour
{
    [SerializeField] private UnityEvent m_eventToFire;


    private void Awake()
    {
        #if !UNITY_EDITOR
          m_eventToFire?.Invoke();
        #endif
    }
}