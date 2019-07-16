namespace TWF
{
    /// <summary>
    /// A voter that checks whether the building model candidate resource needs are met.
    /// If they are, it votes 1, otherwise, 0.
    /// To determine whether the resource needs are met, for each needed resource, it:
    /// 1) picks a random tile on the map that is able to provide the resource
    /// 2) checks the connectivity between that tile and the candidate tile
    /// 3) votes 1 if there's connectivity, 0 otherwise
    /// </summary>
    public class BuildingResourceVoter : IBuildingDevelopmentVoter
    {
        private readonly BuildingConnectionFinder buildingConnectionFinder;

        public BuildingResourceVoter(BuildingConnectionFinder buildingConnectionFinder)
        {
            this.buildingConnectionFinder = buildingConnectionFinder;
        }

        public void OnNewWorld(IWorldView worldView)
        {
            // no-op
        }

        public double Vote(Vector pos, int buildingModel)
        {
            foreach (var connection in this.buildingConnectionFinder.FindConnections(pos, buildingModel))
            {
                // connection is a pair where the value represents the connecting vector corresponding to the building resource production
                if (connection.Item2 == null)
                {
                    return 0;
                }
            }

            // if we reach here, all building resource productions found a connection
            return 1;
        }
    }
}
