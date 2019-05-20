using System;
using System.Linq;
using System.Collections.Generic;
using TWF.Map.Building;
using TWF.Map.Tile;
using TWF.Map.Accessors;

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

        public Action<World> execute(IWorldView worldView)
        {
            List<(Vector, ITileView)> positionsToBuild = worldView
                .ToAllPositions()
                .ToTilePositionTuples()
                .Where((t) => constructibleZones.Contains(t.Item2.Zone) && doBuild())
                .ToList();

            return (world) =>
            {
                foreach ((Vector, Tile) t in positionsToBuild.Where((t) => null == world.GetTile(t.Item1).Building))
                {
                    world.SetTileBuilding(
                        new Building(t.Item2.Zone, random(), new Dictionary<UsageType, Usage>()),
                        t.Item1);
                }
            };
        }
    }
}
