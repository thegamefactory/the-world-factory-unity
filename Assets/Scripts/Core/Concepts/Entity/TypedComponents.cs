namespace TWF
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Richly typed components that can be attached to entities.
    /// </summary>
    /// <typeparam name="T">The type of value that can be attached to each entity.</typeparam>
    public class TypedComponents<T> : IReadOnlyTypedComponents<T>
    {
        private readonly List<T> values = new List<T>();
        private readonly Func<T> defaultProvider;

        public TypedComponents(string name, Func<T> defaultProvider)
        {
            this.Name = name;
            this.defaultProvider = defaultProvider;
        }

        public string Name { get; }

        public void SetComponent(int entityId, T value)
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
                this.SetComponent(entityId, this.defaultProvider());
            }

            return this.values[entityId];
        }

        public IEnumerable<int> GetMatchingEntities(Func<T, bool> predicate)
        {
            for (int entityId = 0; entityId < this.values.Count; entityId++)
            {
                if (predicate(this.values[entityId]))
                {
                    yield return entityId;
                }
            }
        }
    }
}
