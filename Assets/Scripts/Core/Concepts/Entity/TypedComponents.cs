namespace TWF
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Richly typed components that can be attached to entities.
    /// </summary>
    internal class TypedComponent<T> : IReadOnlyTypedComponents<T>
    {
        private readonly List<T> values = new List<T>();
        private readonly Func<T> defaultProvider;

        public TypedComponent(string name, Func<T> defaultProvider)
        {
            this.Name = name;
            this.defaultProvider = defaultProvider;
        }

        public string Name { get; }

        public void AttachComponent(int entityId, T value)
        {
            while (this.values.Count <= entityId)
            {
                this.values.Add(this.defaultProvider());
            }

            this.values[entityId] = value;
        }

        public T GetComponent(int entityId)
        {
            if (this.values.Count <= entityId)
            {
                this.AttachComponent(entityId, this.defaultProvider());
            }

            return this.values[entityId];
        }
    }
}
