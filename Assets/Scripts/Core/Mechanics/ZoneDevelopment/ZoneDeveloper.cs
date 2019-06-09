namespace TWF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// This agent is creating buildings on unoccupied zoned tiles.
    /// </summary>
    public class ZoneDeveloper : IAgent
    {
        private readonly Func<bool> doBuild;
        private readonly Func<int> random;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZoneDeveloper"/> class.
        /// </summary>
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
            Contract.Requires(worldView != null);
            var developableZones = worldView.Rules.Zones.GetMarkerComponents(Zones.DEVELOPABLE);

            List<(Vector, int)> positionsToBuild = worldView.GetZoneMapView()
                .ToAllPositions()
                .ToPositionTuples()
                .Where((z) => developableZones.IsMarked(z.Item2) && this.doBuild())
                .ToList();

            return (world) =>
            {
                IMap<Building> buildingMap = world.GetBuildingMap();
                foreach (var z in positionsToBuild.Where((z) => buildingMap[z.Item1] == null))
                {
                    buildingMap[z.Item1] = new Building(this.random());
                }
            };
        }
    }
}
