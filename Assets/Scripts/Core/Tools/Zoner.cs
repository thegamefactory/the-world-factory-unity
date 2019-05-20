using System;
using System.Collections.Generic;
using System.Linq;
using TWF.Map.Tile;

namespace TWF.Tool
{
    /// <summary>
    /// A tool behavior that attempts to set the zone of the input positions to the zone corresponding to the given modifier.
    /// </summary>
    public class Zoner : IToolBehavior
    {
        public ToolBehaviorType ToolBehaviorType { get; } = ToolBehaviorType.ZONER;
        private HashSet<Modifier> Modifiers { get; } = new HashSet<Modifier> {
            new Modifier(TileZone.EMPTY.ToString()),
            new Modifier(TileZone.FARMLAND.ToString()),
            new Modifier(TileZone.RESIDENTIAL.ToString()),
            new Modifier(TileZone.ROAD.ToString())
        };

        public Action<World> CreateActions(Modifier modifier, IEnumerable<Vector> inputPositions)
        {
            TileZone zone;
            if (!Enum.TryParse(modifier.Identifier, out zone))
            {
                throw new InvalidOperationException("Zone modifier is invalid: " + modifier);
            }

            return (world) =>
            {
                foreach (var pos in inputPositions)
                {
                    world.SetTileZone(zone, pos);
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
            PreviewOutcome.Builder builder = PreviewOutcome.builder();
            foreach (Vector pos in inputPositions)
            {
                bool possible = null == worldView.GetTile(pos).Entity && TileTerrain.LAND == worldView.GetTile(pos).Terrain;
                builder.WithPositionOutcome(pos, possible ? ToolOutcome.SUCCESS : ToolOutcome.FAILURE);
            }
            return builder.Build();
        }
    }
}
