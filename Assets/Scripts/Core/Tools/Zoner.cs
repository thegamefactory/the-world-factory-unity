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
            Vector first = inputPositions.First.Value;
            Vector second = inputPositions.Last.Value;
            Vector min = new Vector(Math.Min(first.X, second.X), Math.Min(first.Y, second.Y));
            Vector max = new Vector(Math.Max(first.X, second.X), Math.Max(first.Y, second.Y));
            for (int x = min.X; x <= max.X; x++)
            {
                for (int y = min.Y; y <= max.Y; y++)
                {
                    map.GetTile(x, y).Type = newType;
                }
            }
            return ToolOutcome.SUCCESS;
        }

        public ISet<Modifier> GetModifiers()
        {
            return Modifiers;
        }

        public ToolOutcome Preview(LinkedList<Vector> inputPositions, TileMap map, Modifier modifier)
        {
            if (!Modifiers.Contains(modifier) || inputPositions.Count != 2)
            {
                return ToolOutcome.FAILURE;
            }
            return ToolOutcome.SUCCESS;
        }
    }
}
