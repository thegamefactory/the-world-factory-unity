using System;
using System.Collections.Generic;
using TWF.Map;
using TWF.Tool;

namespace TWF.Tool
{
    public class RoadBuilder : ITool
    {
        ISet<Modifier> Modifiers = new HashSet<Modifier>();

        public ToolType ToolType { get; } = ToolType.ROAD_BUILDER;

        public ToolOutcome Apply(LinkedList<Vector> inputPositions, TileMap map, Modifier modifier)
        {
            if (Preview(inputPositions, map, modifier) != ToolOutcome.SUCCESS)
            {
                return ToolOutcome.FAILURE;
            }
            foreach (Vector pos in GetRoadPositions(inputPositions))
            {
                map.GetTile(pos).Type = Tile.Zone.ROAD;
            }
            return ToolOutcome.SUCCESS;
        }

        public ISet<Modifier> GetModifiers()
        {
            return Modifiers;
        }

        public ToolOutcome Preview(LinkedList<Vector> inputPositions, TileMap map, Modifier modifier)
        {
            if (inputPositions.Count != 2)
            {
                return ToolOutcome.FAILURE;
            }
            foreach (Vector pos in GetRoadPositions(inputPositions))
            {
                if (map.GetTile(pos).Type != Tile.Zone.EMPTY) return ToolOutcome.FAILURE;
            }
            return ToolOutcome.SUCCESS;
        }

        private LinkedList<Vector> GetRoadPositions(LinkedList<Vector> inputPositions)
        {
            LinkedList<Vector> result = new LinkedList<Vector>();
            Vector first = inputPositions.First.Value;
            Vector second = inputPositions.Last.Value;
            Vector horizontalStart = first.X < second.X ? first : second;
            Vector horizontalEnd = first.X < second.X ? second : first;
            for (int x = horizontalStart.X; x <= horizontalEnd.X; x++)
            {
                result.AddLast(new Vector(x, first.Y));
            }
            Vector verticalStart = first.Y < second.Y ? first : second;
            Vector verticalEnd = first.Y < second.Y ? second : first;
            for (int y = verticalStart.Y; y <= verticalEnd.Y; y++)
            {
                result.AddLast(new Vector(second.X, y));
            }
            return result;
        }
    }
}
