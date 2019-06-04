using TWF;
using UnityEngine;
using UnityEngine.Assertions;
public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float noisePeriod;
    public float waterThreshold;
    public bool autoUpdate;
    public int seed;

    public void OnValidate()
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
        if (noisePeriod < 1.0f)
        {
            noisePeriod = 1.0f;
        }
    }

    public void Start()
    {
#if DEBUG
        Assert.raiseExceptions = true;
#endif
        Generate();
    }

    public void Generate()
    {
        System.Random random = new System.Random(seed);
        ITerrainGenerator terrainGenerator;
        if (waterThreshold <= 0)
        {
            terrainGenerator = new UniformMapGenerator(Terrains.LAND);
        }
        else if (waterThreshold <= 1)
        {
            terrainGenerator = new WaterThresholdTerrainGenerator(
                new PerlinNoiseGenerator(noisePeriod, (float)random.NextDouble(), (float)random.NextDouble()),
                waterThreshold);
        }
        else
        {
            terrainGenerator = new UniformMapGenerator(Terrains.WATER);
        }

        Root.Size = new Vector(mapWidth, mapHeight);
        Root.TerrainGenerator = terrainGenerator;
        Root.Recreate();

        if (autoUpdate)
        {
            MapDisplay mapDisplay = GetComponent<MapDisplay>();
            mapDisplay.Update();
        }
    }
}
