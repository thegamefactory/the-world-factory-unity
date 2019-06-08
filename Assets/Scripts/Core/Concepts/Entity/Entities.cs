using System;
using System.Collections.Generic;

namespace TWF
{
    /// <summary>
    /// A registry of entities.
    /// An entity is modeled by an integer and a string identifier.
    /// The entity can be fetched by both but repeated lookups or iterations across all the entities should use the efficient integer identifier.
    /// 
    /// The entity is a bare interface. To provide meaning to entities, implementers must attach them components.
    /// </summary>
    public class Entities : IReadOnlyEntities
    {
        public string Name { get; }
        readonly Dictionary<string, int> nameIdLookup = new Dictionary<string, int>();
        readonly List<string> names = new List<string>();
        readonly Dictionary<string, IReadOnlyComponents> componentRegistries = new Dictionary<string, IReadOnlyComponents>();

        public Entities(string name)
        {
            Name = name;
        }

        public int Register(string entityName)
        {
            int id = names.Count;
            names.Add(entityName);
            nameIdLookup[entityName] = id;
            return id;
        }

        public void Extend(IReadOnlyComponents component)
        {
            if (null == component.Name)
            {
                throw new NullReferenceException();
            }
            if (componentRegistries.ContainsKey(component.Name))
            {
                throw new ArgumentException("Duplicate component name: " + component.Name);
            }
            componentRegistries[component.Name] = component;
        }

        public string this[int id] => names[id];
        public int this[string name] => nameIdLookup[name];

        public IReadOnlyComponents GetComponents(string componentName)
        {
            if (!componentRegistries.ContainsKey(componentName))
            {
                throw new KeyNotFoundException(Name + "[" + componentName + "]");
            }
            return componentRegistries[componentName];
        }

        public int NumberOfEntities => names.Count;
    }
}
