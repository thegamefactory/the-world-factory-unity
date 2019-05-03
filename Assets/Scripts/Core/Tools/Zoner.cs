using System;
using System.Collections.Generic;
using TWF.Map;

namespace TWF.Tool
{
    public class Zoner : ITool
    {
        public ToolType ToolType { get; } = ToolType.ZONER;
        public HashSet<Modifier> Modifiers { get; } = new HashSet<Modifier> {
            new Modifier(Tile.Zone.EMPTY.ToString()),
            new Modifier(Tile.Zone.RESIDENTIAL.ToString())
        };

        public ToolOutcome Apply(LinkedList<Vector> inputPositions, TileMap map, Modifier modifier)
        {
            if (Preview(inputPositions, map, modifier) != ToolOutcome.SUCCESS)
            {
                return ToolOutcome.FAILURE;
            }
            Tile.Zone newType;
            if (!Enum.TryParse(modifier.Identifier, out newType))
            {
                throw new InvalidOperationException("Zone modifier is invalid: " + modifier);
            }
            map.GetTile(inputPositions.First.Value).Type = newType;
            return ToolOutcome.SUCCESS;
        }

        public ISet<Modifier> GetModifiers()
        {
            return Modifiers;
        }

        public ToolOutcome Preview(LinkedList<Vector> inputPositions, TileMap map, Modifier modifier)
        {
            if (!Modifiers.Contains(modifier) || inputPositions.Count != 1)
            {
                return ToolOutcome.FAILURE;
            }
            return ToolOutcome.SUCCESS;
        }
    }
}
