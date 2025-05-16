using UnityEngine;

public class VisualizeCameraFrustum : MonoBehaviour
{
    [Header("Frustum Visualization")]
    [Tooltip("The color to draw the frustum lines.")]
    public Color frustumColor = Color.green;
    [Tooltip("Enable or disable frustum visualization in the editor.")]
    public bool visualizeInEditor = true;

    private Camera targetCamera;

    void OnEnable()
    {
        // Find the Camera component attached to this GameObject, or the main camera if none.
        targetCamera = GetComponent<Camera>();
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
            if (targetCamera == null)
            {
                Debug.LogError("No Camera found to visualize the frustum for.");
                enabled = false; // Disable the script if no camera is found.
                return;
            }
        }
    }

    // Draw the frustum gizmo in the editor.
    private void OnDrawGizmos()
    {
        if (!visualizeInEditor || targetCamera == null) return;

        Gizmos.color = frustumColor;
        Gizmos.matrix = targetCamera.transform.localToWorldMatrix;
        Gizmos.DrawFrustum(Vector3.zero, targetCamera.fieldOfView, targetCamera.farClipPlane, targetCamera.nearClipPlane, targetCamera.aspect);
    }

    // Optionally, you can draw the frustum in the game view as well (for debugging).
    // This will draw lines using Debug.DrawLine, which are only visible in the Scene view unless "Gizmos" are enabled in the Game view.
    private void Update()
    {
        if (targetCamera == null) return;

        Vector3[] frustumCorners = new Vector3[5]; // We'll reuse the near clip plane corners

        // Get the world space corners of the near clip plane
        targetCamera.CalculateFrustumCorners(new Rect(0, 0, 1, 1), targetCamera.nearClipPlane, Camera.MonoOrStereoscopicEye.Mono, frustumCorners);
        Vector3 nearTopLeft = transform.TransformPoint(frustumCorners[0]);
        Vector3 nearTopRight = transform.TransformPoint(frustumCorners[1]);
        Vector3 nearBottomRight = transform.TransformPoint(frustumCorners[2]);
        Vector3 nearBottomLeft = transform.TransformPoint(frustumCorners[3]);
        Vector3 cameraPosition = targetCamera.transform.position;

        // Draw the near clip plane lines
        Debug.DrawLine(nearTopLeft, nearTopRight, frustumColor);
        Debug.DrawLine(nearTopRight, nearBottomRight, frustumColor);
        Debug.DrawLine(nearBottomRight, nearBottomLeft, frustumColor);
        Debug.DrawLine(nearBottomLeft, nearTopLeft, frustumColor);

        // Get the world space corners of the far clip plane
        targetCamera.CalculateFrustumCorners(new Rect(0, 0, 1, 1), targetCamera.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, frustumCorners);
        Vector3 farTopLeft = transform.TransformPoint(frustumCorners[0]);
        Vector3 farTopRight = transform.TransformPoint(frustumCorners[1]);
        Vector3 farBottomRight = transform.TransformPoint(frustumCorners[2]);
        Vector3 farBottomLeft = transform.TransformPoint(frustumCorners[3]);

        // Draw the far clip plane lines
        Debug.DrawLine(farTopLeft, farTopRight, frustumColor);
        Debug.DrawLine(farTopRight, farBottomRight, frustumColor);
        Debug.DrawLine(farBottomRight, farBottomLeft, frustumColor);
        Debug.DrawLine(farBottomLeft, farTopLeft, frustumColor);

        // Draw lines connecting the near and far clip planes
        Debug.DrawLine(nearTopLeft, farTopLeft, frustumColor);
        Debug.DrawLine(nearTopRight, farTopRight, frustumColor);
        Debug.DrawLine(nearBottomRight, farBottomRight, frustumColor);
        Debug.DrawLine(nearBottomLeft, farBottomLeft, frustumColor);
    }
}