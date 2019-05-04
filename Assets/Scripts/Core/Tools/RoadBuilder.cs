using System;
using System.Collections.Generic;
using System.Linq;
using TWF.Map;

namespace TWF.Tool
{
    public class RoadBuilder : IToolBehavior
    {
        public ToolBehaviorType ToolBehaviorType { get; } = ToolBehaviorType.ROAD_BUILDER;

        public Action<GameService> CreateActions(IEnumerable<Vector> inputPositions, Modifier modifier)
        {
            return (gameService) =>
            {
                foreach (Vector pos in GetRoadPositions(inputPositions))
                {
                    gameService.SetTileZone(Tile.TileZone.ROAD, pos);
                }
            };
        }

        public ToolOutcome Validate(IGameState gameState, IEnumerable<Vector> inputPositions, Modifier modifier)
        {
            if (inputPositions.Count() != 2)
            {
                return ToolOutcome.FAILURE;
            }
            return inputPositions.All((pos) =>
            {
                return null == gameState.GetEntity(pos) && Tile.TileTerrain.LAND == gameState.GetTile(pos).Terrain;
            }) ? ToolOutcome.SUCCESS : ToolOutcome.FAILURE;
        }

        private LinkedList<Vector> GetRoadPositions(IEnumerable<Vector> inputPositions)
        {
            LinkedList<Vector> result = new LinkedList<Vector>();
            var first = inputPositions.First();
            var second = inputPositions.Last();
            var horizontalStart = first.X < second.X ? first : second;
            var horizontalEnd = first.X < second.X ? second : first;
            for (int x = horizontalStart.X; x <= horizontalEnd.X; x++)
            {
                result.AddLast(new Vector(x, first.Y));
            }
            var verticalStart = first.Y < second.Y ? first : second;
            var verticalEnd = first.Y < second.Y ? second : first;
            for (int y = verticalStart.Y; y <= verticalEnd.Y; y++)
            {
                result.AddLast(new Vector(second.X, y));
            }
            return result;
        }
    }
}
