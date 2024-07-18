using UnityEngine;

public class PeopleWalker : MonoBehaviour
{
    public GameObject personPrefab; // Assign the 3D Cube prefab
    public float spawnInterval = 0.1f; // Time between each spawn
    private float timer;
    public Transform groundPlane; // Reference to the 'Ground' plane

    // Start is called before the first frame update
    void Start()
    {
        timer = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnPerson();
            timer = spawnInterval;
        }
    }

    void SpawnPerson()
    {
        // Get the bounds of the ground plane
        Renderer groundRenderer = groundPlane.GetComponent<Renderer>();
        float minX = groundRenderer.bounds.min.x;
        float maxX = groundRenderer.bounds.max.x;
        float minZ = groundRenderer.bounds.min.z;
        float maxZ = groundRenderer.bounds.max.z;

        // Randomize the spawn position within the bounds of the ground plane
        float xPosition = Random.Range(minX, maxX);
        float zPosition = Random.Range(minZ, maxZ);
        Vector3 spawnPosition = new Vector3(xPosition, 0, zPosition);

        // Instantiate the person prefab
        GameObject person = Instantiate(personPrefab, spawnPosition, Quaternion.identity);

        // Assign movement details to the person for circular motion
        person.AddComponent<PersonMovement>().Initialize(spawnPosition);

        // Assign random scale to the person
        float scale = Random.Range(0.5f, 1f);
        person.transform.localScale = new Vector3(scale, scale, scale);
    }
}

class PersonMovement : MonoBehaviour
{
    private Vector3 centerPosition; // Center of the circular path
    private float speed; // Movement speed
    private float radius; // Radius of the circular path
    private float angle; // Current angle on the circular path

    public void Initialize(Vector3 spawnPosition)
    {
        centerPosition = spawnPosition;
        speed = Random.Range(5.0f, 40.0f); // Adjust min and max speed as needed
        radius = Random.Range(5.0f, 20.0f); // Adjust min and max radius as needed
        angle = Random.Range(0, 360); // Random starting angle
    }

    void Update()
    {
        // Update the angle based on speed
        angle += speed * Time.deltaTime;

        // Convert angle to radians
        float radian = angle * Mathf.Deg2Rad;

        // Calculate new position on the circular path
        float x = centerPosition.x + Mathf.Cos(radian) * radius;
        float z = centerPosition.z + Mathf.Sin(radian) * radius;

        transform.position = new Vector3(x, transform.position.y, z);
    }
}
