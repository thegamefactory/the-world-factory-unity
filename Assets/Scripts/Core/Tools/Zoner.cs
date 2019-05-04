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
            new Modifier(Tile.TileZone.RESIDENTIAL.ToString())
        };

        public Action<GameService> CreateActions(IEnumerable<Vector> inputPositions, Modifier modifier)
        {
            Tile.TileZone zone;
            if (!Enum.TryParse(modifier.Identifier, out zone))
            {
                throw new InvalidOperationException("Zone modifier is invalid: " + modifier);
            }

            return (gameservice) =>
            {
                foreach (var inputPosition in inputPositions)
                {
                    gameservice.SetTileZone(zone, inputPosition);
                }
            };
        }

        public ToolOutcome Validate(IGameState gameState, IEnumerable<Vector> inputPositions, Modifier modifier)
        {
            if (!Modifiers.Contains(modifier))
            {
                return ToolOutcome.FAILURE;
            }
            return inputPositions.All((pos) =>
            {
                return null == gameState.GetEntity(pos) && Tile.TileTerrain.LAND == gameState.GetTile(pos).Terrain;
            }) ? ToolOutcome.SUCCESS : ToolOutcome.FAILURE;
        }
    }
}
