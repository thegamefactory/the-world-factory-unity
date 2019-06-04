using System;
using System.Linq;
using System.Collections.Generic;

namespace TWF
{
    /// <summary>
    /// This agent is creating buildings on unoccupied zoned tiles.
    /// </summary>
    public class ZoneDeveloper : IAgent
    {
        private Func<bool> doBuild;
        private Func<int> random;

        /// <param name="doBuild">A function called for each unoccupied zoned tile; if it returns true, the agent creates a building.</param>
        /// <param name="random">A function that generates random numbers.</param>
        public ZoneDeveloper(Func<bool> doBuild, Func<int> random)
        {
            this.doBuild = doBuild;
            this.random = random;
        }

        public string Name => "Constructor";

        public Action<World> execute(IWorldView worldView)
        {
            var developableZones = worldView.Zones.GetMarkerComponentRegistry(Zones.DEVELOPABLE);

            List<(Vector, int)> positionsToBuild = worldView.GetZoneMapView()
                .ToAllPositions()
                .ToPositionTuples()
                .Where((z) => developableZones.IsMarked(z.Item2) && doBuild())
                .ToList();

            return (world) =>
            {
                IMap<Building> buildingMap = world.GetBuildingMap();
                foreach (var z in positionsToBuild.Where((z) => null == buildingMap[z.Item1]))
                {
                    buildingMap[z.Item1] = new Building(random());
                }
            };
        }
    }
}
