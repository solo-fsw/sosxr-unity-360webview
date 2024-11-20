using UnityEngine;


namespace mrstruijk.debugging
{
    public class ToggleMaterials : MonoBehaviour
    {
        public Renderer Renderer;
        public Material NewMaterial;
        private bool _original;
        private Material _startingMaterial;


        private void Awake()
        {
            if (Renderer == null)
            {
                Renderer = GetComponent<Renderer>();
            }

            _startingMaterial = Renderer.material;
            _original = true;
        }


        private void Start()
        {
            if (NewMaterial == null)
            {
                NewMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            }
        }


        public void ChangeMaterial()
        {
            if (_original)
            {
                Renderer.material = NewMaterial;
            }
            else
            {
                Renderer.material = _startingMaterial;
            }

            Debug.Log("Changed material");

            _original = !_original;
        }
    }
}