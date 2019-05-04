using System;
using System.Collections.Generic;
using TWF.Agent;
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

        public Action<GameService> CreateActions(LinkedList<Vector> inputPositions, Modifier modifier)
        {
            Tile.TileZone zone;
            if (!Enum.TryParse(modifier.Identifier, out zone))
            {
                throw new InvalidOperationException("Zone modifier is invalid: " + modifier);
            }

            return (gameservice) =>
            {
                gameservice.SetTileZone(zone, inputPositions.First.Value);
            };
        }

        public ToolOutcome Validate(IGameState gameState, LinkedList<Vector> inputPositions, Modifier modifier)
        {
            if (!Modifiers.Contains(modifier) || inputPositions.Count != 1)
            {
                return ToolOutcome.FAILURE;
            }
            return ToolOutcome.SUCCESS;
        }
    }
}
