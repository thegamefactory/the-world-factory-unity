namespace TWF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A graph of vectors built from a generic map view. Each tile in the map is a node which may or may not be connected to the graph.
    /// </summary>
    public class MapGraph<T> : IGraph<Vector>
    {
        private readonly IMapView<T> mapView;
        private readonly Func<T, bool> graphMembershipTester;

        public MapGraph(IMapView<T> mapView, Func<T, bool> graphMembershipTester)
        {
            this.mapView = mapView;
            this.graphMembershipTester = graphMembershipTester;
        }

        public bool IsConnected(Vector position)
        {
            return this.graphMembershipTester(this.mapView[position]);
        }

        public IEnumerable<(Vector, int)> GetWeighedConnections(Vector position)
        {
            return this.mapView.GetNeighbors(position)
                .Where(this.IsConnected)
                .Select(p => (p, 1));
        }
    }
}
