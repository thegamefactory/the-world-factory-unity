using System.Collections.Generic;

namespace TWF
{
    public static partial class Zones
    {
        public static string ZONABLE_TERRAINS = "zonableTerrains";

        public static void DefaultZonableTerrainComponent(WorldConfig worldConfig)
        {
            Registry ZoneRegistry = worldConfig.Zones;
            Registry TerrainRegistry = worldConfig.Terrains;
            ZonableOnlyOn zonableOnlyOnLand = new ZonableOnlyOn(TerrainRegistry[Terrains.LAND]);
            TypedComponent<IZonableTerrain> zonableTerrains = new TypedComponent<IZonableTerrain>(ZONABLE_TERRAINS, () => zonableOnlyOnLand);
            zonableTerrains.AttachComponent(ZoneRegistry[Zones.EMPTY], new AlwaysZonable());
            ZoneRegistry.Extend(zonableTerrains);
        }
    }

    public interface IZonableTerrain
    {
        bool IsZonable(int terrainId);
    }

    public class ZonableOnlyOn : IZonableTerrain
    {
        int zonableTerrainId;

        public ZonableOnlyOn(int zonableTerrainId)
        {
            this.zonableTerrainId = zonableTerrainId;
        }

        public bool IsZonable(int terrainId)
        {
            return terrainId == zonableTerrainId;
        }
    }

    public class AlwaysZonable : IZonableTerrain
    {
        public bool IsZonable(int terrainId)
        {
            return true;
        }
    }
}
