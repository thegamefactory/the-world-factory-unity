using System;
using UnityEngine;
using TWF.Terrain;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float scale;
    public bool autoUpdate;

    void OnValidate()
    {
        if (mapWidth <= 0)
        {
            mapWidth = 1;
        }
        if (mapWidth > 255)
        {
            mapWidth = 255;
        }
        if (mapHeight <= 0)
        {
            mapHeight = 1;
        }
        if (mapHeight > 255)
        {
            mapHeight = 255;
        }
        if (scale < 1.0f)
        {
            scale = 1.0f;
        }
    }

    void Start()
    {
        Generate();
    }
    
    void Update()
    {

    }

    public void Generate()
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];
        NoiseGenerator noiseGenerator = new NoiseGenerator(scale);
        noiseGenerator.Generate(noiseMap);
        MapDisplay mapDisplay = FindObjectOfType<MapDisplay>();
        mapDisplay.Draw(noiseMap);
    }
}
