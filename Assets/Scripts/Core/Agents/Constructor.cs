using System;
using System.Linq;
using System.Collections.Generic;
using TWF.Map;

namespace TWF.Agent
{
    public class Constructor: IAgent
    {
        private TileMap tileMap;
        private Func<bool> doBuild;

        public Constructor(TileMap tileMap, Func<bool> doBuild)
        {
            this.tileMap = tileMap;
            this.doBuild = doBuild;
        }

        public Action<GameService> execute()
        {
            List<Position> positionsToBuild = tileMap.GetTiles()
                .Where((t) => t.Item2.Type == Tile.TileType.RESIDENTIAL && doBuild())
                .Select((t) => t.Item1)
                .ToList();

            return (gameservice) =>
            {
                foreach(Position freePos in positionsToBuild.Where((p) => null == gameservice.GetEntity(p.X, p.Y)))
                {
                    gameservice.SetEntity(new Building(), freePos.X, freePos.Y);
                }
            };
        }
    }
}
