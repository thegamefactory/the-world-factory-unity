using UnityEngine;
using TWF.Generation;
using TWF;
using TWF.Map.Tile;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float noisePeriod;
    public float waterThreshold;
    public bool autoUpdate;
    public int seed;

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
        if (noisePeriod < 1.0f)
        {
            noisePeriod = 1.0f;
        }
    }

    void Start()
    {
        Generate(Root.GameService);
    }

    public void Generate(GameService gameService)
    {
        System.Random random = new System.Random(seed);
        ITileMapGenerator tileMapGenerator;
        if (waterThreshold <= 0)
        {
            tileMapGenerator = new UniformMapGenerator(TileTerrain.LAND);
        }
        else if (waterThreshold <= 1)
        {
            tileMapGenerator = new WaterThresholdTileMapGenerator(
                new PerlinNoiseGenerator(noisePeriod, (float)random.NextDouble(), (float)random.NextDouble()),
                waterThreshold);
        }
        else
        {
            tileMapGenerator = new UniformMapGenerator(TileTerrain.WATER);
        }

        Root.GameService.SetWorld(WorldFactory.Create(new Vector(mapWidth, mapHeight), tileMapGenerator, random));

        if (autoUpdate)
        {
            MapDisplay mapDisplay = GetComponent<MapDisplay>();
            mapDisplay.Update();
        }
    }
}
