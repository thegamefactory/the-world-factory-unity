namespace TWF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Maintains a cache zone id to all building models ids that can be developed on the corresponding zone.
    /// </summary>
    public class ZoneBuildingModels
    {
        private IDictionary<int, int[]> zoneBuildingModels;

        public void OnNewWorld(IWorldView worldView)
        {
            Contract.Requires(worldView != null);

            var zoneBuildingModels = new Dictionary<int, LinkedList<int>>();

            var buildingModelZones = worldView.Rules.BuildingModels.GetTypedComponents<int>(BuildingModels.BuildingModelZoneComponent);
            foreach ((int buildingModelId, int zoneId) in buildingModelZones.GetEntityComponentPairs())
            {
                if (!zoneBuildingModels.ContainsKey(zoneId))
                {
                    zoneBuildingModels[zoneId] = new LinkedList<int>();
                }

                zoneBuildingModels[zoneId].AddLast(buildingModelId);
            }

            this.zoneBuildingModels = new Dictionary<int, int[]>();
            foreach (var keyValuePair in zoneBuildingModels)
            {
                this.zoneBuildingModels[keyValuePair.Key] = new int[keyValuePair.Value.Count];
                zoneBuildingModels[keyValuePair.Key].CopyTo(this.zoneBuildingModels[keyValuePair.Key], 0);
            }
        }

        public int[] GetBuildingModelsForZone(int zoneId)
        {
            if (this.zoneBuildingModels.ContainsKey(zoneId))
            {
                return this.zoneBuildingModels[zoneId];
            }
            else
            {
                return Array.Empty<int>();
            }
        }
    }
}
