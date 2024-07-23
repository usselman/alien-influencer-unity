using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public GameObject personPrefab; // Assign the 3D Cube prefab
    public GameObject enemyTowerPrefab; // Assign the Enemy Tower prefab
    public TerrainGenerator terrainGenerator; // Reference to the TerrainGenerator
    public int maxPeople = 500; // Maximum number of people to spawn
    public int maxTowers = 50; // Maximum number of enemy towers to spawn
    public float minTowerDistance = 10f; // Minimum distance between enemy towers
    private Terrain terrain;
    private Vector3 terrainCenter;
    private List<Vector3> towerPositions = new List<Vector3>(); // List to track tower positions

    private void Start()
    {
        StartCoroutine(InitializeSpawner());
    }

    private IEnumerator InitializeSpawner()
    {
        while ((terrain = terrainGenerator.GetTerrain()) == null)
        {
            yield return null; // Wait until the terrain is generated
        }

        // Get the bounds of the terrain
        TerrainData terrainData = terrain.terrainData;
        float minXPosition = terrain.transform.position.x;
        float maxXPosition = terrain.transform.position.x + terrainData.size.x;
        float minZPosition = terrain.transform.position.z;
        float maxZPosition = terrain.transform.position.z + terrainData.size.z;
        terrainCenter = new Vector3((minXPosition + maxXPosition) / 2, 0, (minZPosition + maxZPosition) / 2);

        for (int i = 0; i < maxPeople; i++)
        {
            SpawnPerson();
        }

        for (int i = 0; i < maxTowers; i++)
        {
            SpawnEnemyTower();
        }
    }

    void SpawnPerson()
    {
        if (terrain == null)
            return;

        // Get the terrain size
        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainSize = terrainData.size;

        // Randomize the spawn position within the bounds of the terrain, centered around the terrain center
        float xPosition = Random.Range(terrainCenter.x - terrainSize.x / 2, terrainCenter.x + terrainSize.x / 2);
        float zPosition = Random.Range(terrainCenter.z - terrainSize.z / 2, terrainCenter.z + terrainSize.z / 2);
        float yPosition = terrain.SampleHeight(new Vector3(xPosition, 0, zPosition)) + terrain.transform.position.y;

        Vector3 spawnPosition = new Vector3(xPosition, yPosition, zPosition);

        // Instantiate the person prefab
        GameObject person = Instantiate(personPrefab, spawnPosition, Quaternion.identity);

        // Assign movement details to the person for circular motion
        person.AddComponent<PersonMovement>().Initialize(spawnPosition);

        // Assign random scale to the person
        float scale = Random.Range(0.25f, 1f);
        person.transform.localScale = new Vector3(scale, scale, scale);
    }

    void SpawnEnemyTower()
    {
        if (terrain == null)
            return;

        // Get the terrain size
        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainSize = terrainData.size;
        Vector3 spawnPosition;
        bool validPosition = false;

        // Attempt to find a valid spawn position for the tower
        for (int attempts = 0; attempts < 100; attempts++) // Limiting the number of attempts to avoid infinite loops
        {
            float xPosition = Random.Range(terrainCenter.x - terrainSize.x / 2, terrainCenter.x + terrainSize.x / 2);
            float zPosition = Random.Range(terrainCenter.z - terrainSize.z / 2, terrainCenter.z + terrainSize.z / 2);
            float yPosition = terrain.SampleHeight(new Vector3(xPosition, 0, zPosition)) + terrain.transform.position.y;

            spawnPosition = new Vector3(xPosition, yPosition, zPosition);

            // Check if the spawn position is valid
            validPosition = true;
            foreach (Vector3 pos in towerPositions)
            {
                if (Vector3.Distance(spawnPosition, pos) < minTowerDistance)
                {
                    validPosition = false;
                    break;
                }
            }

            if (validPosition)
            {
                towerPositions.Add(spawnPosition);
                Instantiate(enemyTowerPrefab, spawnPosition, Quaternion.identity);
                break;
            }
        }

        if (!validPosition)
        {
            Debug.LogWarning("Failed to find a valid position for an enemy tower after 100 attempts.");
        }
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
