using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UfoMovement : MonoBehaviour
{
    public float speed = 20.0f; // Movement speed of the UFO
    public float tiltAmount = 10.0f; // Maximum tilt angle
    public TerrainGenerator terrainGenerator; // Reference to the TerrainGenerator
    private Terrain terrain; // Reference to the terrain
    private Rigidbody rb; // Reference to the UFO's Rigidbody
    private float constantHeight = 10f; // Constant height above the terrain for the UFO

    private float minXPosition;
    private float maxXPosition;
    private float minZPosition;
    private float maxZPosition;
    private Vector3 terrainCenter;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Get the instantiated terrain
        terrain = terrainGenerator.GetTerrain();

        if (terrain != null)
        {
            // Get the bounds of the terrain
            TerrainData terrainData = terrain.terrainData;
            minXPosition = terrain.transform.position.x;
            maxXPosition = terrain.transform.position.x + terrainData.size.x;
            minZPosition = terrain.transform.position.z;
            maxZPosition = terrain.transform.position.z + terrainData.size.z;
            terrainCenter = new Vector3((minXPosition + maxXPosition) / 2, 0, (minZPosition + maxZPosition) / 2);

            // Set the UFO initial position to the center of the terrain
            float terrainHeight = terrain.SampleHeight(terrainCenter) + terrain.transform.position.y;
            rb.position = new Vector3(terrainCenter.x, terrainHeight + constantHeight, terrainCenter.z);
        }
        else
        {
            Debug.LogError("Terrain not found!");
        }
    }

    private void FixedUpdate()
    {
        if (terrain == null)
            return;

        // Handle movement with momentum
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Movement vector
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * speed;
        rb.AddForce(movement);

        // Clamp the UFO's position within bounds
        float clampedX = Mathf.Clamp(rb.position.x, minXPosition, maxXPosition);
        float clampedZ = Mathf.Clamp(rb.position.z, minZPosition, maxZPosition);

        // Get the terrain height at the UFO's position and add the constant height offset
        float terrainHeight = terrain.SampleHeight(new Vector3(clampedX, 0, clampedZ)) + terrain.transform.position.y;
        float yPosition = terrainHeight + constantHeight;

        rb.position = new Vector3(clampedX, yPosition, clampedZ);

        // Tilt the UFO based on movement direction
        Vector3 tilt = new Vector3(verticalInput * tiltAmount, 0, -horizontalInput * tiltAmount);
        rb.rotation = Quaternion.Euler(tilt);
    }
}
