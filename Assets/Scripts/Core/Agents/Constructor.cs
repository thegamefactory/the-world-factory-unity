using System;
using System.Linq;
using System.Collections.Generic;
using TWF.Map;

namespace TWF.Agent
{
    public class Constructor : IAgent
    {
        private Func<bool> doBuild;

        public Constructor(Func<bool> doBuild)
        {
            this.doBuild = doBuild;
        }

        public string Name => "Constructor";

        public Action<GameService> execute(IGameState gameState)
        {
            List<Vector> positionsToBuild = gameState.GetTiles()
                .Where((t) => t.Item2.Zone == Tile.TileZone.RESIDENTIAL && doBuild())
                .Select((t) => t.Item1)
                .ToList();

            return (gameservice) =>
            {
                foreach (Vector freePos in positionsToBuild.Where((p) => null == gameservice.GetEntity(p.X, p.Y)))
                {
                    gameservice.SetEntity(new Building(), freePos.X, freePos.Y);
                }
            };
        }
    }
}
