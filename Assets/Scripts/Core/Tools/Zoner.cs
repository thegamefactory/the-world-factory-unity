using System;
using System.Collections.Generic;
using System.Linq;
using TWF.Map;

namespace TWF.Tool
{
    /// <summary>
    /// A tool behavior that attempts to set the zone of the input positions to the zone corresponding to the given modifier.
    /// </summary>
    public class Zoner : IToolBehavior
    {
        public ToolBehaviorType ToolBehaviorType { get; } = ToolBehaviorType.ZONER;
        private HashSet<Modifier> Modifiers { get; } = new HashSet<Modifier> {
            new Modifier(Tile.TileZone.EMPTY.ToString()),
            new Modifier(Tile.TileZone.RESIDENTIAL.ToString()),
            new Modifier(Tile.TileZone.ROAD.ToString())
        };

        public Action<GameService> CreateActions(Modifier modifier, IEnumerable<Vector> inputPositions)
        {
            Tile.TileZone zone;
            if (!Enum.TryParse(modifier.Identifier, out zone))
            {
                throw new InvalidOperationException("Zone modifier is invalid: " + modifier);
            }

            return (gameservice) =>
            {
                foreach (var pos in inputPositions)
                {
                    gameservice.SetTileZone(zone, pos);
                }
            };
        }

        public PreviewOutcome Preview(IGameState gameState, Modifier modifier, IEnumerable<Vector> inputPositions)
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
                bool possible = null == gameState.GetEntity(pos) && Tile.TileTerrain.LAND == gameState.GetTile(pos).Terrain;
                builder.WithPositionOutcome(pos, possible ? ToolOutcome.SUCCESS : ToolOutcome.FAILURE);
            }
            return builder.Build();
        }
    }
}
