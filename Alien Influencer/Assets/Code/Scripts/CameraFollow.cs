using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The UFO's Transform
    public float smoothSpeed = 0.125f; // Adjust for smoother following
    public Vector3 offset; // Offset from the UFO

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Optionally, if you want the camera to always look at the UFO or another point:
            //transform.LookAt(target);
        }
    }
}
