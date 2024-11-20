using UnityEngine;


namespace mrstruijk.SimpleHelpers
{
    /// <summary>
    ///     Attach to gameobject, and enable bool dontDestroy, to make sure it stays loaded between scenes
    /// </summary>
    public class DontDestroyOnLoad : MonoBehaviour
    {
        [SerializeField] private bool m_dontDestroy = true;


        protected virtual void Awake()
        {
            if (m_dontDestroy)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}