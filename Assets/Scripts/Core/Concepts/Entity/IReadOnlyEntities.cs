namespace TWF
{
    /// <summary>
    /// A registry of entities.
    /// An entity is modeled by an integer and a string identifier.
    /// The entity can be fetched by both but repeated lookups or iterations across all the entities should use the efficient integer identifier.
    ///
    /// The entity is a bare interface. To provide meaning to entities, implementers must attach them components.
    /// </summary>
    public interface IReadOnlyEntities
    {
        string Name { get; }

        int NumberOfEntities { get; }

        string this[int id] { get; }

        int this[string name] { get; }

        IReadOnlyComponents GetComponents(string componentName);
    }
}
