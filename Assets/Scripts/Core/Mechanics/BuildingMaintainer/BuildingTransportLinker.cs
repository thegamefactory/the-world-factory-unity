namespace TWF
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// The linker finds the nearest transport node that the building can use for pathfinding.
    /// It updates the TransportLink component of the building.
    /// </summary>
    public class BuildingTransportLinker
    {
        private readonly BuildingTransportLinkFinder buildingTransportLinkFinder;

        private IReadOnlyTypedComponents<Vector> buildingLocation;
        private TypedComponents<Vector?> buildingTransportLink;

        public BuildingTransportLinker(BuildingTransportLinkFinder buildingTransportLinkFinder)
        {
            this.buildingTransportLinkFinder = buildingTransportLinkFinder;
        }

        public void OnNewWorld(World world)
        {
            Contract.Requires(world != null);

            this.buildingLocation = world.Buildings.GetTypedComponents<Vector>(Buildings.LocationComponent);
            this.buildingTransportLink = world.Buildings.GetMutableTypedComponents<Vector?>(Buildings.TransportLinkComponent);
        }

        public Vector? GetOrUpdateTransportLink(int buildingId)
        {
            Vector pos = this.buildingLocation[buildingId];

            Vector? link = this.buildingTransportLink[buildingId];
            if (link.HasValue && this.buildingTransportLinkFinder.IsLinked(pos))
            {
                return link;
            }

            var newLink = this.buildingTransportLinkFinder.FindTransportLink(pos);
            if (newLink.HasValue)
            {
                this.buildingTransportLink[buildingId] = newLink;
            }

            return newLink;
        }
    }
}
