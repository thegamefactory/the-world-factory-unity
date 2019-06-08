using TWF;
using TWF.Graphics;
using UnityEngine;
using UnityEngine.Assertions;
public class MapGenerator : MonoBehaviour
{
    public int MapWidth;
    public int MapHeight;
    public float NoisePeriod;
    public float WaterThreshold;
    public bool AutoUpdate;
    public int Seed;

    public void OnValidate()
    {
        if (MapWidth <= 0)
        {
            MapWidth = 1;
        }
        if (MapWidth > 255)
        {
            MapWidth = 255;
        }
        if (MapHeight <= 0)
        {
            MapHeight = 1;
        }
        if (MapHeight > 255)
        {
            MapHeight = 255;
        }
        if (NoisePeriod < 1.0f)
        {
            NoisePeriod = 1.0f;
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
        System.Random random = new System.Random(Seed);
        ITerrainGenerator terrainGenerator;
        if (WaterThreshold <= 0)
        {
            terrainGenerator = new UniformMapGenerator(Terrains.LAND);
        }
        else if (WaterThreshold <= 1)
        {
            terrainGenerator = new WaterThresholdTerrainGenerator(
                new PerlinNoiseGenerator(NoisePeriod, (float)random.NextDouble(), (float)random.NextDouble()),
                WaterThreshold);
        }
        else
        {
            terrainGenerator = new UniformMapGenerator(Terrains.WATER);
        }

        var worldFactory = Root.GameService.WorldFactory;
        worldFactory.Size = new Vector(MapWidth, MapHeight);
        worldFactory.TerrainGenerator = terrainGenerator;
        Root.GameService.NewWorld();

        if (AutoUpdate)
        {
            MapDisplay mapDisplay = GetComponent<MapDisplay>();
            mapDisplay.Update();
        }
    }
}
