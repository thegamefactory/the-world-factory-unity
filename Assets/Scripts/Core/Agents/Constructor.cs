using System;
using System.Linq;
using System.Collections.Generic;
using TWF.Building;
using TWF.Map;

namespace TWF.Agent
{
    /// <summary>
    /// This agent is creating buildings on unoccupied zoned tiles.
    /// </summary>
    public class Constructor : IAgent
    {
        private ISet<TileZone> constructibleZones;
        private Func<bool> doBuild;
        private Func<int> seeder;

        /// <param name="doBuild">A function called for each unoccupied zoned tile; if it returns true, the agent creates a building.</param>
        public Constructor(ISet<TileZone> constructibleZones, Func<bool> doBuild, Func<int> seeder)
        {
            this.constructibleZones = constructibleZones;
            this.doBuild = doBuild;
            this.seeder = seeder;
        }

        public string Name => "Constructor";

        public Action<GameService> execute(IGameState gameState)
        {
            List<(Vector, Tile)> positionsToBuild = gameState.GetTiles()
                .Where((t) => constructibleZones.Contains(t.Item2.Zone) && doBuild())
                .ToList();

            return (gameservice) =>
            {
                foreach ((Vector, Tile) t in positionsToBuild.Where((t) => null == gameservice.GetEntity(t.Item1)))
                {
                    gameservice.SetEntity(
                        new Building.Building(t.Item2.Zone, seeder(), new Dictionary<UsageType, Usage>()),
                        t.Item1);
                }
            };
        }
    }
}
