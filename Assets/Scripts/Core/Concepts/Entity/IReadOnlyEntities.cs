namespace TWF
{
    /// <summary>
    /// A registry of entities.
    /// An entity is modeled by an integer.
    /// The entity is a bare interface. To provide meaning to entities, implementers must attach them components.
    /// </summary>
    public interface IReadOnlyEntities
    {
        int NumberOfEntities { get; }

        IReadOnlyComponents GetComponents(string componentName);
    }
}
