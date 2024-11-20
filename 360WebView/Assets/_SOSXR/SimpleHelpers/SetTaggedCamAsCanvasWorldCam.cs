using SOSXR.EnhancedLogger;
using UnityEngine;


namespace mrstruijk.SimpleHelpers
{
    [RequireComponent(typeof(Canvas))]
    public class SetTaggedCamAsCanvasWorldCam : MonoBehaviour
    {
        [TagSelector]
        [SerializeField] private string m_camTag = "MainCamera";
        private Canvas _canvas;


        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }


        private void Start()
        {
            FindWorldCamera();
        }


        private void Update()
        {
            FindWorldCamera();
        }


        private void FindWorldCamera()
        {
            if (_canvas.worldCamera == null && GameObject.FindWithTag(m_camTag) != null)
            {
                _canvas.worldCamera = GameObject.FindWithTag(m_camTag).GetComponentInChildren<Camera>();

                return;
            }

            this.Success("Found world cam!");

            enabled = false; // Disable this component
        }
    }
}