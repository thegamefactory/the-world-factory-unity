namespace TWF
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A tool behavior that sets the zone of the input positions.
    /// </summary>
    public class Zoner : IToolBehavior
    {
        public Zoner(NamedEntity zone)
        {
            this.Zone = zone;
        }

        public NamedEntity Zone { get; }

        public string Name => NameFromZone(this.Zone.Name);

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
                    zoneMap[pos] = this.Zone.Id;
                }
            };
        }

        public ToolPreviewOutcome Preview(IWorldView worldView, IEnumerable<Vector> inputPositions)
        {
            IMapView<int> terrainMap = worldView.GetTerrainMapView();
            IMapView<Building> buildingMap = worldView.GetBuildingMapView();
            IZonableTerrain zonableTerrains = worldView.Rules.Zones.GetTypedComponents<IZonableTerrain>(Zones.ZONABLE_TERRAINS).GetComponent(this.Zone.Id);

            ToolPreviewOutcome.PreviewOutcomeBuilder builder = ToolPreviewOutcome.Builder();
            foreach (Vector pos in inputPositions)
            {
                bool possible = buildingMap[pos] == null && zonableTerrains.IsZonable(terrainMap[pos]);
                builder.WithPositionOutcome(pos, possible ? ToolOutcome.SUCCESS : ToolOutcome.FAILURE);
            }

            return builder.Build();
        }
    }
}
