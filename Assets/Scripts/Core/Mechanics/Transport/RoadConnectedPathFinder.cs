namespace TWF
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// A path finder which first checks if the origin or the destination is either a road or adjacent to a road.
    /// Then it delegates the path finding from the road start to the road implementation to the delegate path finder.
    /// </summary>
    public class RoadConnectedPathFinder : IPathFinder<Vector>
    {
        private readonly IPathFinder<Vector> impl;
        private IMapView<int> zoneMap;
        private int roadZoneId;

        public RoadConnectedPathFinder(IPathFinder<Vector> impl)
        {
            this.impl = impl;
        }

        public void OnNewWorld(IWorldView worldView)
        {
            Contract.Requires(worldView != null);

            this.zoneMap = worldView.GetMapView<int>(MapTypes.Zone);
            this.roadZoneId = worldView.Rules.Zones[Zones.Road];
        }

        public bool FindPath(Vector origin, Vector destination, int maxCost, ref Path<Vector> path)
        {
            Vector? roadOrigin = this.FindConnection(origin);
            if (!roadOrigin.HasValue)
            {
                return false;
            }

            Vector? roadDestination = this.FindConnection(destination);
            if (!roadDestination.HasValue)
            {
                return false;
            }

            return this.impl.FindPath(roadOrigin.Value, roadDestination.Value, maxCost, ref path);
        }

        private Vector? FindConnection(Vector position)
        {
            foreach (var candidate in this.Candidates(position))
            {
                if (this.IsRoad(candidate))
                {
                    return candidate;
                }
            }

            return null;
        }

        private bool IsRoad(Vector position)
        {
            return this.zoneMap[position] == this.roadZoneId;
        }

        private IEnumerable<Vector> Candidates(Vector position)
        {
            yield return position;
            foreach (var neighbor in this.zoneMap.GetNeighbors(position))
            {
                yield return neighbor;
            }
        }
    }
}
