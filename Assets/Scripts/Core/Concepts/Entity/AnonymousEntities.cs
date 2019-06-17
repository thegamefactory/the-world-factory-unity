namespace TWF
{
    /// <summary>
    /// A registry of entities.
    /// An entity is modeled by an integer and a string identifier.
    /// The entity can be fetched by both but repeated lookups or iterations across all the entities should use the efficient integer identifier.
    ///
    /// The entity is a bare interface. To provide meaning to entities, implementers must attach them components.
    /// </summary>
    public class AnonymousEntities : AbstractEntities, IReadOnlyEntities
    {
        private int maxId = 0;

        public AnonymousEntities(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        public override int NumberOfEntities => 0;

        public int Register()
        {
            return this.maxId++;
        }
    }
}
