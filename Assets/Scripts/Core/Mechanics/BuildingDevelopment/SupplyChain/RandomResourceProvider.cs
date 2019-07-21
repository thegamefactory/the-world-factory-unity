namespace TWF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Minimalistic implementation of a cache which is able to provide a random position in the map that is producing resources now or is expected.
    /// to produce resources in the future.
    ///
    /// The cache is maintained up to date by listening to updates on the zone map and the building map.
    /// </summary>
    public class RandomResourceProvider
    {
        private readonly BuildingTransportLinkFinder buildingTransportLinkFinder;

        private Random random;
        private IDictionary<int, RandomAccessSet<Vector>> producersForResource;
        private IDictionary<int, RandomAccessSet<Vector>> consumersForResource;
        private IMapView<int> buildingMap;
        private IMapView<int> zoneMap;
        private IReadOnlyTypedComponents<BuildingResourceProduction[]> buildingModelsResourceProduction;
        private IReadOnlyTypedComponents<int> defaultZoneBuildingModels;
        private IReadOnlyTypedComponents<Vector?> buildingTransportLinkComponent;

        public RandomResourceProvider(BuildingTransportLinkFinder buildingTransportLinkFinder)
        {
            this.buildingTransportLinkFinder = buildingTransportLinkFinder;
        }

        public void OnNewWorld(IWorldView worldView)
        {
            Contract.Requires(worldView != null);

            this.random = worldView.Rules.Random;

            this.producersForResource = new Dictionary<int, RandomAccessSet<Vector>>();
            this.consumersForResource = new Dictionary<int, RandomAccessSet<Vector>>();

            this.buildingModelsResourceProduction = worldView.Rules.BuildingModels.GetTypedComponents<BuildingResourceProduction[]>(
                BuildingModels.BuildingModelResourceProductionComponent);
            this.defaultZoneBuildingModels = worldView.Rules.Zones.GetTypedComponents<int>(Zones.DefaultBuildingModel);
            this.buildingMap = worldView.GetMapView<int>(MapTypes.Building);
            this.zoneMap = worldView.GetMapView<int>(MapTypes.Zone);

            this.zoneMap.RegisterListener(this.OnZoneMapUpdate);
            worldView.GetMapView<int>(MapTypes.Building).RegisterListener(this.OnBuildingMapUpdate);

            this.buildingTransportLinkComponent = worldView.Buildings.GetTypedComponents<Vector?>(Buildings.TransportLinkComponent);
        }

        public Vector? GetRandomSupplyChainLink(BuildingResourceProduction buildingResourceProduction)
        {
            var resourceRegister = this.GetResourceRegister(buildingResourceProduction);
            if (resourceRegister == null)
            {
                return null;
            }

            RandomAccessSet<Vector> producersForResource = GetOrCreateRegisterStorage(resourceRegister, buildingResourceProduction.ResourceId);
            int candidatesSize = producersForResource.Size;
            if (candidatesSize > 0)
            {
                return this.TransportLinkForResult(producersForResource[this.random.Next(candidatesSize)]);
            }
            else
            {
                return null;
            }
        }

        private static RandomAccessSet<Vector> GetOrCreateRegisterStorage(IDictionary<int, RandomAccessSet<Vector>> resourceRegister, int resourceId)
        {
            if (!resourceRegister.TryGetValue(resourceId, out RandomAccessSet<Vector> result))
            {
                result = new RandomAccessSet<Vector>(64);
                resourceRegister[resourceId] = result;
            }

            return result;
        }

        private Vector? TransportLinkForResult(Vector position)
        {
            int buildingId = this.buildingMap[position];
            if (buildingId != MapTypes.NoBuilding)
            {
                return this.buildingTransportLinkComponent[buildingId];
            }
            else
            {
                return this.buildingTransportLinkFinder.FindTransportLink(position);
            }
        }

        private void OnZoneMapUpdate(Vector position, int previousZoneId, int nextZoneId)
        {
            this.DeregisterProvider(position, previousZoneId);
            this.RegisterProvider(position, nextZoneId);
        }

        private void OnBuildingMapUpdate(Vector position, int previousBuilding, int nextBuilding)
        {
            if (previousBuilding == MapTypes.NoBuilding && nextBuilding != MapTypes.NoBuilding)
            {
                this.DeregisterProvider(position, this.zoneMap[position]);
            }

            if (previousBuilding != MapTypes.NoBuilding && nextBuilding == MapTypes.NoBuilding)
            {
                this.RegisterProvider(position, this.zoneMap[position]);
            }
        }

        private void DeregisterProvider(Vector position, int zoneId)
        {
            this.ForEachResourceProducerDo(position, zoneId, (r, v) => r.Remove(v));
        }

        private void RegisterProvider(Vector position, int zoneId)
        {
            this.ForEachResourceProducerDo(position, zoneId, (r, v) => r.Add(v));
        }

        private void ForEachResourceProducerDo(Vector position, int zoneId, Action<RandomAccessSet<Vector>, Vector> action)
        {
            var resourceProductions = this.BuildingResourceProductionForZone(zoneId);
            foreach (var rp in resourceProductions)
            {
                IDictionary<int, RandomAccessSet<Vector>> resourceRegister = this.GetResourceRegister(rp);
                if (resourceRegister != null)
                {
                    action.Invoke(GetOrCreateRegisterStorage(resourceRegister, rp.ResourceId), position);
                }
            }
        }

        private IDictionary<int, RandomAccessSet<Vector>> GetResourceRegister(BuildingResourceProduction buildingResourceProduction)
        {
            if (buildingResourceProduction.IsProducer())
            {
                return this.producersForResource;
            }
            else if (buildingResourceProduction.IsConsumer())
            {
                return this.consumersForResource;
            }

            return null;
        }

        private BuildingResourceProduction[] BuildingResourceProductionForZone(int zoneId)
        {
            int defaultBuildingModel = this.defaultZoneBuildingModels[zoneId];
            if (defaultBuildingModel != BuildingModels.NoModel)
            {
                return this.buildingModelsResourceProduction[defaultBuildingModel];
            }
            else
            {
                return Array.Empty<BuildingResourceProduction>();
            }
        }
    }
}
