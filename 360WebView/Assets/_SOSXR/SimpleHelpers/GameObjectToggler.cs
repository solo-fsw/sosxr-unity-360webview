using System.Collections.Generic;
using UnityEngine;


namespace mrstruijk.SimpleHelpers
{
    public class GameObjectToggler : MonoBehaviour
    {
        [SerializeField] private List<GameObject> m_toToggle;


        public void Toggle()
        {
            foreach (var go in m_toToggle)
            {
                go.SetActive(!go.activeInHierarchy);
            }
        }


        public void Toggle(bool enable)
        {
            foreach (var go in m_toToggle)
            {
                go.SetActive(enable);
            }
        }


        public void Enable()
        {
            Toggle(true);
        }


        public void Disable()
        {
            Toggle(false);
        }
    }
}