﻿namespace TWF
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A concrete graph implementation which connects every tile adjacent to a road.
    /// Neighboring tiles are connected with a cost of 1.
    /// </summary>
    public class RoadGraph : IGraph<Vector>
    {
        private readonly IMapView<int> zoneMap;
        private readonly int roadZoneId;

        public RoadGraph(IMapView<int> zoneMap, int roadZoneId)
        {
            this.zoneMap = zoneMap;
            this.roadZoneId = roadZoneId;
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
