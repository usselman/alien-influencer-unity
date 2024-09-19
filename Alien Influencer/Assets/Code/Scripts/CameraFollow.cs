using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float positionSmoothSpeed = 0.125f;
    public float rotationSmoothSpeed = 0.1f;
    public Vector3 offset;

    private Vector3 currentVelocity;

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + target.TransformDirection(offset);

            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, positionSmoothSpeed);
            transform.position = smoothedPosition;

            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothSpeed);
        }
    }
}
