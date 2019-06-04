using System;
using System.Collections.Generic;

namespace TWF
{
    /// <summary>
    /// A tool behavior that attempts to set the zone of the input positions.
    /// </summary>
    public class Zoner : IToolBehavior
    {
        public Zoner(NamedEntity zone)
        {
            Zone = zone;
        }

        public NamedEntity Zone { get; }

        public string Name => NameFromZone(Zone.Name);

        public static string NameFromZone(string zoneName)
        {
            return zoneName + "Zoner";
        }

        public Action<World> CreateActions(IEnumerable<Vector> inputPositions)
        {
            return (world) =>
            {
                IMap<int> zoneMap = world.GetZoneMap();
                foreach (var pos in inputPositions)
                {
                    zoneMap[pos] = Zone.Id;
                }
            };
        }

        public PreviewOutcome Preview(IWorldView worldView, IEnumerable<Vector> inputPositions)
        {
            IMapView<int> terrainMap = worldView.GetTerrainMapView();
            IMapView<Building> buildingMap = worldView.GetBuildingMapView();
            IZonableTerrain zonableTerrains = worldView.Zones.GetTypedComponentRegistry<IZonableTerrain>(Zones.ZONABLE_TERRAINS).GetComponent(Zone.Id);

            PreviewOutcome.Builder builder = PreviewOutcome.builder();
            foreach (Vector pos in inputPositions)
            {
                bool possible = null == buildingMap[pos] && zonableTerrains.IsZonable(terrainMap[pos]);
                builder.WithPositionOutcome(pos, possible ? ToolOutcome.SUCCESS : ToolOutcome.FAILURE);
            }
            return builder.Build();
        }
    }
}
