namespace TWF
{
    using System.Collections.Generic;

    /// <summary>
    /// A simple component that is used to mark entities; this enable a simple component existence check for the entity.
    /// </summary>
    public interface IReadOnlyMarkerComponentRegistry : IReadOnlyComponents
    {
        /// <summary>
        /// Returns whether the entity is marked or not by this component.
        /// </summary>
        bool IsMarked(int entityId);

        /// <summary>
        /// Returns the list of all the entities that are marked by this component.
        /// </summary>
        LinkedList<int> MarkedEntities();
    }
}
