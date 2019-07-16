namespace TWF
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    /// <summary>
    /// A concrete graph implementation which connects every tile adjacent to a road.
    /// Neighboring tiles are connected with a cost of 1.
    /// </summary>
    public class RoadGraph : IGraph<Vector>
    {
        private IMapView<int> zoneMap;
        private int roadZoneId;

        public void OnNewWorld(IWorldView worldView)
        {
            Contract.Requires(worldView != null);

            this.zoneMap = worldView.GetMapView<int>(MapTypes.Zone);
            this.roadZoneId = worldView.Rules.Zones[Zones.Road];
        }

        public IEnumerable<(Vector, int)> GetWeighedConnections(Vector position)
        {
            return this.zoneMap.GetNeighbors(position)
                .Where(this.IsConnected)
                .Select(p => (p, 1));
        }

        public bool IsConnected(Vector position)
        {
            return this.zoneMap[position] == this.roadZoneId;
        }
    }
}
