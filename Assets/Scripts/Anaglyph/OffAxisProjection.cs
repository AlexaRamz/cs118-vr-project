using UnityEngine;

public class OffAxisProjection : MonoBehaviour
{
    public bool isLeftEye;
    public float convergenceDistance = 10f;
    public float eyeOffset = 0.03f; // Half of the interaxial distance

    private void Start()
    {
        Camera cam = GetComponent<Camera>();
        Matrix4x4 newProjectionMatrix = OffAxisProjectionMatrix(cam);
        cam.projectionMatrix = newProjectionMatrix;
    }

    private Matrix4x4 OffAxisProjectionMatrix(Camera cam)
    {
        float near = cam.nearClipPlane;
        float far = cam.farClipPlane;
        float fov = cam.fieldOfView;
        float aspect = cam.aspect;

        // Calculate frustrum width and height at near plane
        float height = near * Mathf.Tan(fov * Mathf.Deg2Rad / 2);
        float width = height * aspect;

        // Determine the horizontal shift needed to converge at the convergenceDistance
        float offset = eyeOffset * near / convergenceDistance;

        // Determine new frustrum boundaries
        float left;
        float right;
        float bottom = -height;
        float top = height;

        if (isLeftEye)
        {
            // For left camera, shift the view to the right
            left = -width + offset;
            right = width + offset;
        }
        else
        {
            // For right camera, shift the view to the left
            left = -width - offset;
            right = width - offset;
        }

        Matrix4x4 projectionMatrix = Matrix4x4.Frustum(left, right, bottom, top, near, far);
        return projectionMatrix;
    }
}
