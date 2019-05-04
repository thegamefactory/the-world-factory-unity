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
                Vector first = inputPositions.First();
                Vector second = inputPositions.Last();
                Vector min = new Vector(Math.Min(first.X, second.X), Math.Min(first.Y, second.Y));
                Vector max = new Vector(Math.Max(first.X, second.X), Math.Max(first.Y, second.Y));
                for (int x = min.X; x <= max.X; x++)
                {
                    for (int y = min.Y; y <= max.Y; y++)
                    {
                        gameservice.SetTileZone(zone, x, y);
                    }
                }

            };
        }

        public ToolOutcome Validate(IGameState gameState, IEnumerable<Vector> inputPositions, Modifier modifier)
        {
            if (!Modifiers.Contains(modifier) || inputPositions.Count() != 2)
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
