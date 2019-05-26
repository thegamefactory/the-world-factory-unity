using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TWF
{
    /// <summary>
    /// A tool behavior that attempts to set the zone of the input positions.
    /// </summary>
    public class Zoner : IToolBehavior
    {
        public Zoner(Zone zone)
        {
            Zone = zone;
            Debug.Assert(zone.HasTrait(ManuallyZonable.Instance.GetType()));
        }

        public Zone Zone { get; }

        public string Name => Zone.ZonerName();

        public Action<World> CreateActions(IEnumerable<Vector> inputPositions)
        {
            return (world) =>
            {
                IMap<Zone> zoneMap = world.GetZoneMap();
                foreach (var pos in inputPositions)
                {
                    zoneMap[pos] = Zone;
                }
            };
        }

        public PreviewOutcome Preview(IWorldView worldView, IEnumerable<Vector> inputPositions)
        {
            IMapView<Terrain> terrainMap = worldView.GetTerrainMapView();
            IMapView<Building> buildingMap = worldView.GetBuildingMapView();

            PreviewOutcome.Builder builder = PreviewOutcome.builder();
            foreach (Vector pos in inputPositions)
            {
                bool possible = null == buildingMap[pos] && Terrain.LAND == terrainMap[pos];
                builder.WithPositionOutcome(pos, possible ? ToolOutcome.SUCCESS : ToolOutcome.FAILURE);
            }
            return builder.Build();
        }
    }

    static class ZonerExtensions
    {
        public static Zoner Zoner(this Zone zone)
        {
            return new Zoner(zone);
        }

        public static String ZonerName(this Zone zone)
        {
            return zone.Name + "Zoner";
        }
    }
}
