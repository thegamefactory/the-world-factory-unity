namespace TWF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// A registry of entities.
    /// An entity is modeled by an integer and a string identifier.
    /// The entity can be fetched by both but repeated lookups or iterations across all the entities should use the efficient integer identifier.
    ///
    /// The entity is a bare interface. To provide meaning to entities, implementers must attach them components.
    /// </summary>
    public class Entities : IReadOnlyEntities
    {
        private readonly Dictionary<string, int> nameIdLookup = new Dictionary<string, int>();
        private readonly List<string> names = new List<string>();
        private readonly Dictionary<string, IReadOnlyComponents> componentRegistries = new Dictionary<string, IReadOnlyComponents>();

        public Entities(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        public int NumberOfEntities => this.names.Count;

        public string this[int id] => this.names[id];

        public int this[string name] => this.nameIdLookup[name];

        public int Register(string entityName)
        {
            int id = this.names.Count;
            this.names.Add(entityName);
            this.nameIdLookup[entityName] = id;
            return id;
        }

        public void Extend(IReadOnlyComponents component)
        {
            Contract.Requires(component != null);
            Contract.Requires(component.Name != null);

            if (this.componentRegistries.ContainsKey(component.Name))
            {
                throw new ArgumentException("Duplicate component name: " + component.Name);
            }

            this.componentRegistries[component.Name] = component;
        }

        public IReadOnlyComponents GetComponents(string componentName)
        {
            if (!this.componentRegistries.ContainsKey(componentName))
            {
                throw new KeyNotFoundException(this.Name + "[" + componentName + "]");
            }

            return this.componentRegistries[componentName];
        }
    }
}
