using System;
using System.Collections.Generic;

namespace TWF
{
    public class Registry : IReadOnlyRegistry
    {
        public string Name { get; }
        readonly Dictionary<string, int> nameIdLookup = new Dictionary<string, int>();
        readonly List<string> names = new List<string>();
        readonly Dictionary<string, IComponentRegistry> componentRegistries = new Dictionary<string, IComponentRegistry>();

        public Registry(string name)
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

        public void Extend(IComponentRegistry component)
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

        public IComponentRegistry GetComponentRegistry(string componentName)
        {
            if (!componentRegistries.ContainsKey(componentName))
            {
                throw new KeyNotFoundException(Name + "[" + componentName + "]");
            }
            return componentRegistries[componentName];
        }

        public IMarkerComponentRegistry GetMarkerComponentRegistry(string componentName)
        {
            TwfDebug.Assert(GetComponentRegistry(componentName) is IMarkerComponentRegistry);
            return GetComponentRegistry(componentName) as IMarkerComponentRegistry;
        }

        public ITypedComponentRegistry<T> GetTypedComponentRegistry<T>(string componentName)
        {
            TwfDebug.Assert(GetComponentRegistry(componentName) is ITypedComponentRegistry<T>);
            return GetComponentRegistry(componentName) as ITypedComponentRegistry<T>;
        }

        public int NumberOfEntities => names.Count;
    }
}
