namespace TWF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Base class to define an entities registry. See related interfaces.
    /// </summary>
    public abstract class AbstractEntities : IExtendableEntities, IReadOnlyEntities
    {
        private readonly Dictionary<string, IReadOnlyComponents> componentRegistries = new Dictionary<string, IReadOnlyComponents>();

        public abstract int NumberOfEntities { get; }

        public abstract string Name { get; }

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
            return this.componentRegistries[componentName];
        }
    }
}
