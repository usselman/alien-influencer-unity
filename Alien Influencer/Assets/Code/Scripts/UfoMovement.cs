using UnityEngine;

public class UfoMovement : MonoBehaviour
{
    public float speed = 20.0f; // Movement speed of the UFO
    public float laneChangeSpeed = 25.0f; // Speed of changing lanes
    private float targetZPosition = 0; // Target Z position for lane switching

    // Define bounds for the UFO's x position
    private float minXPosition = -80f;
    private float maxXPosition = 80f;

    private void Update()
    {
        // Handle left-right movement
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 lateralMovement = new Vector3(horizontalInput, 0, 0) * speed * Time.deltaTime;

        // Calculate new position and ensure it's within bounds
        Vector3 newPosition = transform.position + lateralMovement;
        newPosition.x = Mathf.Clamp(newPosition.x, minXPosition, maxXPosition);

        // Apply the lateral movement within bounds
        transform.position = newPosition;

        // Handle up-down keys for changing lanes
        if (Input.GetKeyDown(KeyCode.UpArrow) && targetZPosition < 25)
        {
            targetZPosition = 25; // Set target to the background lane
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && targetZPosition > 0)
        {
            targetZPosition = 0; // Set target to the foreground lane
        }

        // Move towards the target Z position
        float step = laneChangeSpeed * Time.deltaTime;
        float newZ = Mathf.MoveTowards(transform.position.z, targetZPosition, step);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    }
}
