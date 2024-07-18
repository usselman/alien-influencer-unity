using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UfoMovement : MonoBehaviour
{
    public float speed = 20.0f; // Movement speed of the UFO
    public float tiltAmount = 10.0f; // Maximum tilt angle
    public Transform groundPlane; // Reference to the 'Ground' plane
    private Rigidbody rb; // Reference to the UFO's Rigidbody
    private float constantHeight = 10f; // Constant height for the UFO

    // Define bounds for the UFO's position
    private float minXPosition;
    private float maxXPosition;
    private float minZPosition;
    private float maxZPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Get the bounds of the ground plane
        Renderer groundRenderer = groundPlane.GetComponent<Renderer>();
        minXPosition = groundRenderer.bounds.min.x;
        maxXPosition = groundRenderer.bounds.max.x;
        minZPosition = groundRenderer.bounds.min.z;
        maxZPosition = groundRenderer.bounds.max.z;
    }

    private void FixedUpdate()
    {
        // Handle movement with momentum
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Movement vector
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * speed;
        rb.AddForce(movement);

        // Clamp the UFO's position within bounds
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, minXPosition, maxXPosition),
            constantHeight,
            Mathf.Clamp(rb.position.z, minZPosition, maxZPosition)
        );

        // Tilt the UFO based on movement direction
        Vector3 tilt = new Vector3(verticalInput * tiltAmount, 0, -horizontalInput * tiltAmount);
        rb.rotation = Quaternion.Euler(tilt);
    }
}
