namespace TWF
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// The building connector is updating the building connections to satisfy the building supply chain.
    /// </summary>
    public class BuildingConnector
    {
        private readonly BuildingTransportLinker buildingTransportLinker;
        private readonly BuildingConnectionFinder buildingConnectionFinder;

        private IReadOnlyTypedComponents<int> buildingModel;
        private IReadOnlyTypedComponents<ISet<int>> buildingConnections;

        public BuildingConnector(BuildingTransportLinker buildingTransportLinker, BuildingConnectionFinder buildingConnectionFinder)
        {
            this.buildingTransportLinker = buildingTransportLinker;
            this.buildingConnectionFinder = buildingConnectionFinder;
        }

        public void OnNewWorld(IWorldView worldView)
        {
            Contract.Requires(worldView != null);

            this.buildingModel = worldView.Buildings.GetTypedComponents<int>(Buildings.BuildingModelComponent);
            this.buildingConnections = worldView.Buildings.GetTypedComponents<ISet<int>>(Buildings.ConnectionsComponent);
        }

        public bool UpdateConnections(int buildingId)
        {
            var transportLink = this.buildingTransportLinker.GetOrUpdateTransportLink(buildingId);
            var connections = this.buildingConnections[buildingId];

            if (!transportLink.HasValue)
            {
                foreach (var connectedBuildingId in this.buildingConnections[buildingId])
                {
                    this.InvalidateConnection(buildingId, connectedBuildingId);
                }

                return false;
            }

            var buildingModel = this.buildingModel[buildingId];

            /*           foreach (var connection in this.buildingConnectionFinder.FindConnections(transportLink.Value, buildingModel))
                       {
                           if !(TestConnection(transportLink, buildingId, connection))
                       }*/

            return true;
        }

        public bool TestConnection(Vector originTransportLink, int originBuildingId, int destinationBuildingId)
        {
            var destinationTransportLink = this.buildingTransportLinker.GetOrUpdateTransportLink(destinationBuildingId);
            if (!destinationTransportLink.HasValue)
            {
                return false;
            }
            else
            {
                // FIXME
                return true;
            }
        }

        public void InvalidateConnection(int buildingId, int connectedBuildingId)
        {
            this.buildingConnections[buildingId].Remove(connectedBuildingId);
            this.buildingConnections[connectedBuildingId].Remove(buildingId);
        }
    }
}
