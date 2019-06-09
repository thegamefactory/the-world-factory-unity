using TWF;
using TWF.Graphics;
using UnityEngine;
using UnityEngine.Assertions;

public class MapGenerator : MonoBehaviour
{
#pragma warning disable CA1051 // Do not declare visible instance fields
    public int MapWidth;
    public int MapHeight;
    public float NoisePeriod;
    public float WaterThreshold;
    public bool AutoUpdate;
    public int Seed;
#pragma warning restore CA1051 // Do not declare visible instance fields

    public void OnValidate()
    {
        if (this.MapWidth <= 0)
        {
            this.MapWidth = 1;
        }

        if (this.MapWidth > 255)
        {
            this.MapWidth = 255;
        }

        if (this.MapHeight <= 0)
        {
            this.MapHeight = 1;
        }

        if (this.MapHeight > 255)
        {
            this.MapHeight = 255;
        }

        if (this.NoisePeriod < 1.0f)
        {
            this.NoisePeriod = 1.0f;
        }
    }

    public void Start()
    {
#if DEBUG
        Assert.raiseExceptions = true;
#endif
        this.Generate();
    }

    public void Generate()
    {
        System.Random random = new System.Random(this.Seed);
        ITerrainGenerator terrainGenerator;
        if (this.WaterThreshold <= 0)
        {
            terrainGenerator = new UniformMapGenerator(Terrains.LAND);
        }
        else if (this.WaterThreshold <= 1)
        {
            terrainGenerator = new WaterThresholdTerrainGenerator(
                new PerlinNoiseGenerator(this.NoisePeriod, (float)random.NextDouble(), (float)random.NextDouble()),
                this.WaterThreshold);
        }
        else
        {
            terrainGenerator = new UniformMapGenerator(Terrains.WATER);
        }

        var worldFactory = Root.GameService.WorldFactory;
        worldFactory.Size = new Vector(this.MapWidth, this.MapHeight);
        worldFactory.TerrainGenerator = terrainGenerator;
        Root.GameService.NewWorld();

        if (this.AutoUpdate)
        {
            MapDisplay mapDisplay = this.GetComponent<MapDisplay>();
            mapDisplay.Update();
        }
    }
}
