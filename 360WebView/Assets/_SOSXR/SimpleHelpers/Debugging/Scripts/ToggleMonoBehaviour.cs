using UnityEngine;


namespace mrstruijk.debugging
{
    public class ToggleMonoBehaviour : MonoBehaviour
    {
        public MonoBehaviour ToToggle;


        public void ToggleThisMonoBehaviour()
        {
            ToToggle.enabled = !ToToggle.isActiveAndEnabled;
        }
    }
}