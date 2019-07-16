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
    ///
    /// Current limitations: the cache only considers undeveloped zone tiles.
    /// Future development needed: need to consider buildings in the map which are not mapped to a resource consumer (and thus are producers now).
    /// </summary>
    public class RandomResourceProvider
    {
        private Random random;
        private IDictionary<int, RandomAccessSet<Vector>> producersForResource;
        private IMapView<int> zoneMap;
        private IReadOnlyTypedComponents<BuildingResourceProduction[]> buildingModelsResourceProduction;
        private IReadOnlyTypedComponents<int> defaultZoneBuildingModels;

        public RandomResourceProvider()
        {
        }

        public void OnNewWorld(IWorldView worldView)
        {
            Contract.Requires(worldView != null);

            this.random = worldView.Rules.Random;
            this.producersForResource = new Dictionary<int, RandomAccessSet<Vector>>();
            this.buildingModelsResourceProduction = worldView.Rules.BuildingModels.GetTypedComponents<BuildingResourceProduction[]>(
                BuildingModels.BuildingModelResourceProductionComponent);
            this.defaultZoneBuildingModels = worldView.Rules.Zones.GetTypedComponents<int>(Zones.DefaultBuildingModel);
            this.zoneMap = worldView.GetMapView<int>(MapTypes.Zone);

            this.zoneMap.RegisterListener(this.OnZoneMapUpdate);
            worldView.GetMapView<int>(MapTypes.Building).RegisterListener(this.OnBuildingMapUpdate);
        }

        public Vector? GetRandomProvider(int resourceId)
        {
            RandomAccessSet<Vector> producersForResource = this.GetOrCreateProducersForResource(resourceId);
            int candidatesSize = producersForResource.Size;
            if (candidatesSize > 0)
            {
                return producersForResource[this.random.Next(candidatesSize)];
            }
            else
            {
                return null;
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
                if (rp.IsProducer())
                {
                    action.Invoke(this.GetOrCreateProducersForResource(rp.ResourceId), position);
                }
            }
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

        private RandomAccessSet<Vector> GetOrCreateProducersForResource(int resourceId)
        {
            if (!this.producersForResource.TryGetValue(resourceId, out RandomAccessSet<Vector> result))
            {
                result = new RandomAccessSet<Vector>(64);
                this.producersForResource[resourceId] = result;
            }

            return result;
        }
    }
}
