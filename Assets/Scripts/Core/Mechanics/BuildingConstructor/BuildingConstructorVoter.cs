namespace TWF
{
    /// <summary>
    /// Determines, for each candidate tile, which building model it should develop, if any.
    /// It does so by getting the list of all candidate models corresponding to the zone of the tile.
    /// Then it asks the building development voter to vote whether the model should be developed or not.
    /// If any model has a score higher than 0, it picks it. If multiple models have a score higher than 0, it picks the highest score.
    /// </summary>
    public class BuildingConstructorVoter
    {
        private readonly IBuildingDevelopmentVoter buildingDevelopmentVoter;
        private readonly ZoneBuildingModels zoneBuildingModels;
        private IMapView<int> zoneMap;

        public BuildingConstructorVoter(IBuildingDevelopmentVoter tileDevelopmentVoter, ZoneBuildingModels zoneBuildingModels)
        {
            this.buildingDevelopmentVoter = tileDevelopmentVoter;
            this.zoneBuildingModels = zoneBuildingModels;
        }

        public void OnNewWorld(IWorldView worldView)
        {
            this.buildingDevelopmentVoter.OnNewWorld(worldView);
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
                double score = this.buildingDevelopmentVoter.Vote(position, candidateBuildingModel);
                if (score > maxScore)
                {
                    result = candidateBuildingModel;
                }
            }

            return result;
        }
    }
}
