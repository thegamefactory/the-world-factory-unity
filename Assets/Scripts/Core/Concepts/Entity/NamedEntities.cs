namespace TWF
{
    using System.Collections.Generic;

    public class NamedEntities : AbstractEntities, IReadOnlyNamedEntities
    {
        private readonly Dictionary<string, int> nameIdLookup = new Dictionary<string, int>();
        private readonly List<string> names = new List<string>();

        public NamedEntities(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        public override int NumberOfEntities => this.names.Count;

        public string this[int id] => this.names[id];

        public int this[string name] => this.nameIdLookup[name];

        public int Register(string entityName)
        {
            int id = this.names.Count;
            this.names.Add(entityName);
            this.nameIdLookup[entityName] = id;
            return id;
        }
    }
}
