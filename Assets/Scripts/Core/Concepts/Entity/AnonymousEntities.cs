namespace TWF
{
    /// <summary>
    /// A registry of entities.
    /// An anonymous entity is modeled by an integer.
    /// </summary>
    public class AnonymousEntities : AbstractEntities, IReadOnlyEntities
    {
        private int maxId = 0;

        public AnonymousEntities(string name)
        {
            this.Name = name;
        }

        public override string Name { get; }

        public override int NumberOfEntities => 0;

        public int Register()
        {
            return this.maxId++;
        }
    }
}
