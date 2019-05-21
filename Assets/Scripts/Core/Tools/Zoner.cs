using System;
using System.Collections.Generic;
using System.Linq;

namespace TWF
{
    /// <summary>
    /// A tool behavior that attempts to set the zone of the input positions to the zone corresponding to the given modifier.
    /// </summary>
    public class Zoner : IToolBehavior
    {
        public ToolBehaviorType ToolBehaviorType { get; } = ToolBehaviorType.ZONER;
        private HashSet<Modifier> Modifiers { get; } = new HashSet<Modifier> {
            new Modifier(Zone.EMPTY.ToString()),
            new Modifier(Zone.FARMLAND.ToString()),
            new Modifier(Zone.RESIDENTIAL.ToString()),
            new Modifier(Zone.ROAD.ToString())
        };

        public Action<World> CreateActions(Modifier modifier, IEnumerable<Vector> inputPositions)
        {
            Zone zone;
            if (!Enum.TryParse(modifier.Identifier, out zone))
            {
                throw new InvalidOperationException("Zone modifier is invalid: " + modifier);
            }

            return (world) =>
            {
                IMap<Zone> zoneMap = world.GetZoneMap();
                foreach (var pos in inputPositions)
                {
                    zoneMap[pos] = zone;
                }
            };
        }

        public PreviewOutcome Preview(IWorldView worldView, Modifier modifier, IEnumerable<Vector> inputPositions)
        {
            if (!Modifiers.Contains(modifier))
            {
                return PreviewOutcome.builder()
                    .WithOutcomePositions(ToolOutcome.FAILURE, inputPositions.ToList())
                    .Build();
            }

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
}
