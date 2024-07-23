using UnityEngine;
using System.Collections;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject terrainPrefab; // Assign the Terrain prefab
    public float scale = 20f; // Larger scale for smoother hills
    public float heightMultiplier = 2f; // Height multiplier for the terrain elevation
    private GameObject terrainInstance; // Instance of the Terrain
    private Terrain terrain;
    private bool isTerrainGenerated = false;

    void Start()
    {
        if (terrainPrefab != null)
        {
            terrainInstance = Instantiate(terrainPrefab, Vector3.zero, Quaternion.identity);
            terrain = terrainInstance.GetComponent<Terrain>();
            GenerateTerrain();
        }
    }

    public Terrain GetTerrain()
    {
        return isTerrainGenerated ? terrain : null;
    }

    void GenerateTerrain()
    {
        TerrainData terrainData = terrain.terrainData;
        int width = terrainData.heightmapResolution;
        int height = terrainData.heightmapResolution;

        float[,] heights = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = (float)x / width * scale;
                float yCoord = (float)y / height * scale;
                heights[x, y] = Mathf.PerlinNoise(xCoord, yCoord) * heightMultiplier;
            }
        }

        terrainData.SetHeights(0, 0, heights);
        isTerrainGenerated = true;
    }
}
