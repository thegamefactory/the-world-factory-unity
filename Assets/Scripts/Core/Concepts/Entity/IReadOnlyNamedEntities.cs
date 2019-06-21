namespace TWF
{
    /// <summary>
    /// An entities extension that enables to refer to entites by string identifiers (name) on top of integers ids (id).
    /// The entity can be fetched by both but repeated lookups or iterations across all the entities should use the efficient integer identifier.
    /// </summary>
    public interface IReadOnlyNamedEntities : IReadOnlyEntities
    {
        int this[string name] { get; }

        string this[int id] { get; }
    }
}
