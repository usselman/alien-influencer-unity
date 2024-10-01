using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public GameObject personPrefab;
    public GameObject enemyTowerPrefab;
    public Terrain terrain;
    public int maxPeople = 500;
    public int maxTowers = 50;
    public float minTowerDistance = 10f;
    private Vector3 terrainCenter;
    private List<Vector3> towerPositions = new List<Vector3>();

    private void Start()
    {
        InitializeSpawner();
    }

    private void InitializeSpawner()
    {
        if (terrain == null)
        {
            Debug.LogError("Terrain not assigned in the Spawner script!");
            return;
        }

        /* *
        * Getting bounds of the terrain to spawn objects within the terrain bounds
        * */
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

        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainSize = terrainData.size;

        /* *
        * Randomly generating a position within the terrain bounds
        * */
        float xPosition = Random.Range(terrainCenter.x - terrainSize.x / 2, terrainCenter.x + terrainSize.x / 2);
        float zPosition = Random.Range(terrainCenter.z - terrainSize.z / 2, terrainCenter.z + terrainSize.z / 2);
        float yPosition = terrain.SampleHeight(new Vector3(xPosition, 0, zPosition)) + terrain.transform.position.y;

        Vector3 spawnPosition = new Vector3(xPosition, yPosition, zPosition);

        /*
        !! Deprecate soon !!
        */
        GameObject person = Instantiate(personPrefab, spawnPosition, Quaternion.identity);

        /*
        !! Need to implement object pooling to avoid instantiating and destroying objects frequently !!
        */
        //GameObject person = PoolManager.instance.SpawnFromPool("Person", spawnPosition, Quaternion.identity);

        person.AddComponent<PersonMovement>().Initialize(spawnPosition);

        float scale = Random.Range(0.25f, 1f);
        person.transform.localScale = new Vector3(scale, scale, scale);
    }

    void SpawnEnemyTower()
    {
        if (terrain == null)
            return;

        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainSize = terrainData.size;
        Vector3 spawnPosition;
        bool validPosition = false;

        for (int attempts = 0; attempts < 100; attempts++)
        {
            float xPosition = Random.Range(terrainCenter.x - terrainSize.x / 2, terrainCenter.x + terrainSize.x / 2);
            float zPosition = Random.Range(terrainCenter.z - terrainSize.z / 2, terrainCenter.z + terrainSize.z / 2);
            float yPosition = terrain.SampleHeight(new Vector3(xPosition, 0, zPosition)) + terrain.transform.position.y;

            spawnPosition = new Vector3(xPosition, yPosition, zPosition);

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
    private Vector3 centerPosition;
    private float speed;
    private float radius;
    private float angle;

    public void Initialize(Vector3 spawnPosition)
    {
        centerPosition = spawnPosition;
        speed = Random.Range(5.0f, 40.0f);
        radius = Random.Range(5.0f, 20.0f);
        angle = Random.Range(0, 360);
    }

    void Update()
    {
        /* *
        * Moving the person in a circular path around the center position
        * */

        angle += speed * Time.deltaTime;


        float radian = angle * Mathf.Deg2Rad;

        float x = centerPosition.x + Mathf.Cos(radian) * radius;
        float z = centerPosition.z + Mathf.Sin(radian) * radius;

        Vector3 destination = new Vector3(x, transform.position.y, z);

        transform.LookAt(destination);
        transform.position = destination;
    }
}
