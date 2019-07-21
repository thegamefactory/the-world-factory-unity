namespace TWF
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// The transport link finder finds the nearest possible transport node from the building.
    /// If no transport node is available, it returns null.
    /// </summary>
    public class BuildingTransportLinkFinder
    {
        private IGraph<Vector> transportGraph;

        public int MaxDistance { get; set; }

        public void OnNewWorld(IWorldView worldView)
        {
            Contract.Requires(worldView != null);

            this.transportGraph = worldView.GetTransportGraph();
        }

        public bool IsLinked(Vector position)
        {
            return this.transportGraph.IsConnected(position);
        }

        public Vector? FindTransportLink(Vector position)
        {
            for (int i = 0; i <= this.MaxDistance; i++)
            {
                for (int x = -i; x <= i; x++)
                {
                    for (int y = -i; y <= i; y += (x == -i || x == i) ? 1 : i * 2)
                    {
                        Vector testedPosition = position;
                        testedPosition.X += x;
                        testedPosition.Y += y;
                        if (this.transportGraph.IsConnected(testedPosition))
                        {
                            return testedPosition;
                        }
                    }
                }
            }

            return null;
        }
    }
}
