namespace TWF
{
    public class RootTileDevelopmentVoter
    {
        private readonly CombinedTileDevelopmentVoter combinedTileDevelopmentVoter;
        private readonly ZoneBuildingModels zoneBuildingModels;
        private IMapView<int> zoneMap;

        public RootTileDevelopmentVoter(CombinedTileDevelopmentVoter combinedTileDevelopmentVoter, ZoneBuildingModels zoneBuildingModels)
        {
            this.combinedTileDevelopmentVoter = combinedTileDevelopmentVoter;
            this.zoneBuildingModels = zoneBuildingModels;
        }

        public void OnNewWorld(IWorldView worldView)
        {
            this.combinedTileDevelopmentVoter.OnNewWorld(worldView);
            this.zoneBuildingModels.OnNewWorld(worldView);
            this.zoneMap = worldView.GetMapView<int>(MapTypes.Zone);
        }

        public int Vote(Vector position)
        {
            int[] candidateBuildingModels = this.zoneBuildingModels.GetBuildingModelsForZone(this.zoneMap[position]);
            double maxScore = 0;
            int result = -1;

            foreach (var candidateBuildingModel in candidateBuildingModels)
            {
                double score = this.combinedTileDevelopmentVoter.Vote(position, candidateBuildingModel);
                if (score > maxScore)
                {
                    result = candidateBuildingModel;
                }
            }

            return result;
        }
    }
}
