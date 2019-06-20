namespace TWF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

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
            Contract.Requires(inputPositions != null);

            IMapView<int> terrainMap = worldView.GetTerrainMapView();
            IMapView<int> buildingMap = worldView.GetBuildingMapView();
            IZonableTerrain zonableTerrains = worldView.Rules.Zones.GetTypedComponents<IZonableTerrain>(ZonableTerrains.ComponentName).GetComponent(this.Zone.Id);

            PreviewOutcomeBuilder builder = ToolPreviewOutcome.Builder();
            foreach (Vector pos in inputPositions)
            {
                bool possible = buildingMap[pos] == MapTypes.NoBuilding && zonableTerrains.IsZonable(terrainMap[pos]);
                builder.WithPositionOutcome(pos, possible ? ToolOutcome.Success : ToolOutcome.Failure);
            }

            return builder.Build();
        }
    }
}
