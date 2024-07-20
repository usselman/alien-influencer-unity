using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public Terrain terrain;
    public float scale = 0.1f;
    public float heightMultiplier = 5f;

    void Start()
    {
        GenerateTerrain();
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
    }
}
