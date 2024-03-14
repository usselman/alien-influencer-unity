using UnityEngine;

public class PeopleWalker : MonoBehaviour
{
    public GameObject personPrefab; // Assign the 3D Cube prefab
    public float spawnInterval = 0.1f; // Time between each spawn
    private float timer;

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
        // Randomly select one of the two lanes
        float zPosition = Random.Range(0, 2) == 0 ? 0 : 25; // Choose between foreground and background
                                                            // Randomize the z position within the lane
        float zOffset = Random.Range(-3.0f, 3.0f);
        // Choose a side for the person to start from
        float xPosition = Random.Range(0, 2) == 0 ? -120 : 120;
        Vector3 spawnPosition = new Vector3(xPosition, 0, zPosition + zOffset);

        // Instantiate the person prefab
        GameObject person = Instantiate(personPrefab, spawnPosition, Quaternion.identity);
        // Determine the direction the person should move based on the spawn position
        float moveDirection = xPosition < 0 ? 1 : -1; // Move right if spawned on the left, left if spawned on the right

        // Assign movement details to the person
        person.AddComponent<PersonMovement>().Initialize(moveDirection);

        // Assign random scale to the person
        float scale = Random.Range(0.5f, 1f);
        person.transform.localScale = new Vector3(scale, scale, scale);
    }
}

class PersonMovement : MonoBehaviour
{
    public float speed; // Movement speed

    public void Initialize(float moveDirection)
    {
        // Randomize speed
        speed = Random.Range(5.0f, 35.0f); // Adjust min and max speed as needed
        // Apply the movement direction (left or right)
        GetComponent<Rigidbody>().velocity = new Vector3(moveDirection * speed, 0, 0);
    }

    void Update()
    {
        // Check if the person has reached the edge and destroy if true
        if (Mathf.Abs(transform.position.x) > 150)
        {
            Destroy(gameObject);
        }
    }
}
