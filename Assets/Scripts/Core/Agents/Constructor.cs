using System;
using System.Linq;
using System.Collections.Generic;
using TWF.State.Building;
using TWF.State.Tile;
using TWF.State;
using TWF.State.Accessors;

namespace TWF.Agent
{
    /// <summary>
    /// This agent is creating buildings on unoccupied zoned tiles.
    /// </summary>
    public class Constructor : IAgent
    {
        private ISet<TileZone> constructibleZones;
        private Func<bool> doBuild;
        private Func<int> random;

        /// <param name="constructibleZones">A set of zones on which buildings can be created.</param>
        /// <param name="doBuild">A function called for each unoccupied zoned tile; if it returns true, the agent creates a building.</param>
        /// <param name="random">A function that generates random numbers.</param>
        public Constructor(ISet<TileZone> constructibleZones, Func<bool> doBuild, Func<int> random)
        {
            this.constructibleZones = constructibleZones;
            this.doBuild = doBuild;
            this.random = random;
        }

        public string Name => "Constructor";

        public Action<GameState> execute(IGameStateView gameStateView)
        {
            List<(Vector, ITileView)> positionsToBuild = gameStateView
                .ToAllPositions()
                .ToTilePositionTuples()
                .Where((t) => constructibleZones.Contains(t.Item2.Zone) && doBuild())
                .ToList();

            return (gameState) =>
            {
                foreach ((Vector, Tile) t in positionsToBuild.Where((t) => null == gameState.GetTile(t.Item1).Entity))
                {
                    gameState.SetTileEntity(
                        new Building(t.Item2.Zone, random(), new Dictionary<UsageType, Usage>()),
                        t.Item1);
                }
            };
        }
    }
}
