using System;
using System.Linq;
using System.Collections.Generic;
using TWF.Map;

namespace TWF.Agent
{
    /// <summary>
    /// This agent is creating buildings on unoccupied zoned tiles.
    /// </summary>
    public class Constructor : IAgent
    {
        private Func<bool> doBuild;
        private Func<int> seeder;

        /// <param name="doBuild">A function called for each unoccupied zoned tile; if it returns true, the agent creates a building.</param>
        public Constructor(Func<bool> doBuild, Func<int> seeder)
        {
            this.doBuild = doBuild;
            this.seeder = seeder;
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
                    gameservice.SetEntity(new Building(seeder()), freePos.X, freePos.Y);
                }
            };
        }
    }
}
