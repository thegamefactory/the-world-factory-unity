namespace TWF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    /// <summary>
    /// This agent is creating buildings on unoccupied zoned tiles.
    /// </summary>
    public class ZoneDeveloper : IAgent
    {
        private readonly double developmentRate;
        private readonly Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZoneDeveloper"/> class.
        /// </summary>
        /// <param name="developmentRate">A development rate between 0 (no development) and 1 (immediate development)</param>
        /// <param name="random">Provides random value</param>
        public ZoneDeveloper(double developmentRate, Random random)
        {
            Contract.Requires(developmentRate >= 0.0 && developmentRate <= 1.0);

            this.developmentRate = developmentRate;
            this.random = random;
        }

        public string Name => "Constructor";

        public Action<World> Execute(IWorldView worldView)
        {
            Contract.Requires(worldView != null);
            var developableZones = worldView.Rules.Zones.GetTypedComponents<CombinedZoneDevelopmentVoter>(ZoneDevelopmentVoter.ComponentName);

            List<(Vector, int)> positionsToBuild = worldView.GetZoneMapView()
                .ToAllPositions()
                .ToPositionTuples()
                .Where((z) =>
                {
                    var vote = developableZones.GetComponent(z.Item2).Vote(z.Item1);
                    return vote > 0.0 && this.developmentRate * vote > this.random.NextDouble();
                })
                .ToList();

            return (world) =>
            {
                // this is very hard wired, we will need to make it more generic in the future
                IMap<int> buildingMap = world.GetBuildingMap();
                IMap<int> zoneMap = world.GetZoneMap();
                int commercial = world.Rules.Zones[Zones.Commercial];
                int farmland = world.Rules.Zones[Zones.Farmland];
                int residential = world.Rules.Zones[Zones.Residential];
                IReadOnlyNamedEntities allBuildingModels = world.Rules.BuildingModels;
                int farm = allBuildingModels[BuildingModels.Farm];
                int house = allBuildingModels[BuildingModels.House];
                int shop = allBuildingModels[BuildingModels.Shop];
                AnonymousEntities buildings = world.Buildings;
                TypedComponents<int> buildingVariant = buildings.GetMutableTypedComponents<int>(BuildingVariants.Component);
                TypedComponents<int> buildingModels = buildings.GetMutableTypedComponents<int>(BuildingModels.Component);

                foreach (var z in positionsToBuild.Where((z) => buildingMap[z.Item1] == MapTypes.NoBuilding))
                {
                    int buildingModel;
                    int zone = zoneMap[z.Item1];
                    if (zone == commercial)
                    {
                        buildingModel = shop;
                    }
                    else if (zone == farmland)
                    {
                        buildingModel = farm;
                    }
                    else if (zone == residential)
                    {
                        buildingModel = house;
                    }
                    else
                    {
                        throw new ArgumentException("Unexpected zone type: " + world.Rules.Zones[zone]);
                    }

                    int buildingId = buildings.Register();
                    buildingMap[z.Item1] = buildingId;
                    buildingModels.SetComponent(buildingId, buildingModel);
                }
            };
        }
    }
}
