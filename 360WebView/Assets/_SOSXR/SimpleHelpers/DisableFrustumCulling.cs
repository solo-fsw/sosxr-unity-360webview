using SOSXR.EnhancedLogger;
using UnityEngine;


/// <summary>
///     As per: https://allenwp.com/blog/2013/12/19/disabling-frustum-culling-on-a-game-object-in-unity/
/// </summary>
[ExecuteAlways]
public class DisableFrustumCulling : MonoBehaviour
{
    [SerializeField] private Camera m_camera;


    private void Awake()
    {
        if (m_camera != null)
        {
            return;
        }

        m_camera = Camera.main;
    }


    private void Start()
    {
        SetBoundsToCamera();
    }


    private void SetBoundsToCamera()
    {
        if (m_camera == null)
        {
            this.Error("No camera found!");

            return;
        }

        // boundsTarget is the center of the camera's frustum, in world coordinates:
        var trans = m_camera.transform;
        var camPosition = trans.position;
        var normCamForward = Vector3.Normalize(trans.forward);

        var nearClipPlane = m_camera.nearClipPlane;
        var boundsDistance = (m_camera.farClipPlane - nearClipPlane) / 2 + nearClipPlane;

        var boundsTarget = camPosition + normCamForward * boundsDistance;

        // The game object's transform will be applied to the mesh's bounds for frustum culling checking.
        // We need to "undo" this transform by making the boundsTarget relative to the game object's transform:
        var relativeBoundsTarget = transform.InverseTransformPoint(boundsTarget);

        // Set the bounds of the mesh to be a 1x1x1 cube (actually doesn't matter what the size is)
        var filter = GetComponent<MeshFilter>();
        filter.sharedMesh.bounds = new Bounds(relativeBoundsTarget, Vector3.one);
    }
}