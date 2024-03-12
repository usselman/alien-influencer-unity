using UnityEngine;

public class UfoMovement : MonoBehaviour
{
    public float speed = 5.0f; // Movement speed of the UFO
    public float laneChangeSpeed = 10.0f; // Speed of changing lanes
    private float targetZPosition = 0; // Target Z position for lane switching

    private void Update()
    {
        // Handle left-right movement
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 lateralMovement = new Vector3(horizontalInput, 0, 0) * speed * Time.deltaTime;
        transform.Translate(lateralMovement);

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
