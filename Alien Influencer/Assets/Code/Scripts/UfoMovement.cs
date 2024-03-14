using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UfoMovement : MonoBehaviour
{
    public float speed = 20.0f; // Movement speed of the UFO
    public float laneChangeSpeed = 25.0f; // Speed of changing lanes
    public float tiltAmount = 10.0f; // Maximum tilt angle
    private float targetZPosition = 0; // Target Z position for lane switching
    private Rigidbody rb; // Reference to the UFO's Rigidbody

    // Define bounds for the UFO's x position
    private float minXPosition = -80f;
    private float maxXPosition = 80f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Handle up-down keys for changing lanes
        if (Input.GetKeyDown(KeyCode.UpArrow) && targetZPosition < 25)
        {
            targetZPosition = 25; // Set target to the background lane
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && targetZPosition > 0)
        {
            targetZPosition = 0; // Set target to the foreground lane
        }

        float step = laneChangeSpeed * Time.deltaTime;
        float newZ = Mathf.MoveTowards(transform.position.z, targetZPosition, step);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    }

    private void FixedUpdate()
    {
        // Handle left-right movement with momentum
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 force = new Vector3(horizontalInput, 0, 0) * speed;
        rb.AddForce(force);

        // Clamp the UFO's position within bounds
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, minXPosition, maxXPosition), rb.position.y, rb.position.z);

        // Tilt the UFO based on movement direction
        Vector3 tilt = new Vector3(0, 0, -horizontalInput * tiltAmount);
        rb.rotation = Quaternion.Euler(tilt);
    }
}
