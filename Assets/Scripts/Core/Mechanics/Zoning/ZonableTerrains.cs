using System.Collections.Generic;

namespace TWF
{
    public static partial class Zones
    {
        public static string ZONABLE_TERRAINS = "zonableTerrains";

        public static void DefaultZonableTerrainComponent(WorldConfig worldConfig)
        {
            var Zones = worldConfig.Zones;
            var Terrains = worldConfig.Terrains;
            ZonableOnlyOn zonableOnlyOnLand = new ZonableOnlyOn(Terrains[TWF.Terrains.LAND]);
            TypedComponent<IZonableTerrain> zonableTerrains = new TypedComponent<IZonableTerrain>(ZONABLE_TERRAINS, () => zonableOnlyOnLand);
            zonableTerrains.AttachComponent(Zones[TWF.Zones.EMPTY], new AlwaysZonable());
            Zones.Extend(zonableTerrains);
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
