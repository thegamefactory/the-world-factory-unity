namespace TWF
{
    /// <summary>
    /// Determines, for each unoccupied tile, which building model it should develop, if any.
    /// It does so by getting the list of all candidate models corresponding to the zone of the unoccupied tile.
    /// Then it asks the tile development voter the voting score to develop each model.
    /// If any model has a score higher than 0, it picks it. If multiple models have a score higher than 0, it picks the highest score.
    /// </summary>
    public class RootTileDevelopmentVoter
    {
        private readonly ITileDevelopmentVoter tileDevelopmentVoter;
        private readonly ZoneBuildingModels zoneBuildingModels;
        private IMapView<int> zoneMap;

        public RootTileDevelopmentVoter(ITileDevelopmentVoter tileDevelopmentVoter, ZoneBuildingModels zoneBuildingModels)
        {
            this.tileDevelopmentVoter = tileDevelopmentVoter;
            this.zoneBuildingModels = zoneBuildingModels;
        }

        public void OnNewWorld(IWorldView worldView)
        {
            this.tileDevelopmentVoter.OnNewWorld(worldView);
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
                double score = this.tileDevelopmentVoter.Vote(position, candidateBuildingModel);
                if (score > maxScore)
                {
                    result = candidateBuildingModel;
                }
            }

            return result;
        }
    }
}
