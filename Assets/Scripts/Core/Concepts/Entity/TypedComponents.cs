namespace TWF
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Richly typed components that can be attached to entities.
    /// Note that all entities will be attached a default component to it.
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

        public T this[int entityId]
        {
            get
            {
                if (this.values.Count <= entityId)
                {
                    this[entityId] = this.defaultProvider();
                }

                return this.values[entityId];
            }

            set
            {
                while (this.values.Count <= entityId)
                {
                    this.values.Add(this.defaultProvider());
                }

                this.values[entityId] = value;
            }
        }

        /// <summary>
        /// Returns all the entity ids for which the provided predicate on this component is true.
        /// </summary>
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

        public IEnumerable<(int, T)> GetEntityComponentPairs()
        {
            for (int entityId = 0; entityId < this.values.Count; entityId++)
            {
                yield return (entityId, this.values[entityId]);
            }
        }
    }
}
