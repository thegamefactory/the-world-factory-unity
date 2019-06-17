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
        private readonly Func<bool> doBuild;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZoneDeveloper"/> class.
        /// </summary>
        /// <param name="doBuild">A function called for each unoccupied zoned tile; if it returns true, the agent creates a building.</param>
        public ZoneDeveloper(Func<bool> doBuild)
        {
            this.doBuild = doBuild;
        }

        public string Name => "Constructor";

        public Action<World> Execute(IWorldView worldView)
        {
            Contract.Requires(worldView != null);
            var developableZones = worldView.Rules.Zones.GetMarkerComponents(Zones.Developable);

            List<(Vector, int)> positionsToBuild = worldView.GetZoneMapView()
                .ToAllPositions()
                .ToPositionTuples()
                .Where((z) => developableZones.IsMarked(z.Item2) && this.doBuild())
                .ToList();

            return (world) =>
            {
                // this is very hard wired, we will need to make it more generic in the future
                IMap<int> buildingMap = world.GetBuildingMap();
                IMap<int> zoneMap = world.GetZoneMap();
                int residential = world.Rules.Zones[Zones.Residential];
                int farmland = world.Rules.Zones[Zones.Farmland];
                IReadOnlyNamedEntities allBuildingModels = world.Rules.BuildingModels;
                int house = allBuildingModels[BuildingModels.House];
                int farm = allBuildingModels[BuildingModels.Farm];
                AnonymousEntities buildings = world.Buildings;
                TypedComponents<int> buildingVariant = buildings.GetMutableTypedComponents<int>(BuildingVariants.Component);
                TypedComponents<int> buildingModels = buildings.GetMutableTypedComponents<int>(BuildingModels.Component);

                foreach (var z in positionsToBuild.Where((z) => buildingMap[z.Item1] == MapTypes.NoBuilding))
                {
                    int buildingModel;
                    int zone = zoneMap[z.Item1];
                    if (zone == residential)
                    {
                        buildingModel = house;
                    }
                    else if (zone == farmland)
                    {
                        buildingModel = farm;
                    }
                    else
                    {
                        throw new ArgumentException("Unexpected zone type: " + world.Rules.Zones[zone]);
                    }

                    int buildingId = buildings.Register();
                    buildingMap[z.Item1] = buildingId;
                    buildingModels.AttachComponent(buildingId, buildingModel);
                }
            };
        }
    }
}
