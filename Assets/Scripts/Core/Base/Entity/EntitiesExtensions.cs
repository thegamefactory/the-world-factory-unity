namespace TWF
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Static class extending Entities with a bunch of utility methods.
    /// </summary>
    public static class EntitiesExtensions
    {
        /// <summary>
        /// Return the components of the entites corresponding to the given name.
        /// Although the method is strongly typed, it performs a cast. Build time static type checking is not guaranteed.
        /// </summary>
        public static IReadOnlyTypedComponents<T> GetTypedComponents<T>(this IReadOnlyEntities entities, string componentName)
        {
            Contract.Requires(entities != null);
            Contract.Requires(entities.GetComponents(componentName) is IReadOnlyTypedComponents<T>);
            return entities.GetComponents(componentName) as IReadOnlyTypedComponents<T>;
        }

        /// <summary>
        /// Return the components of the entites corresponding to the given name.
        /// The components is mutable, meaning that new components can be attached to entities.
        /// Although the method is strongly typed, it performs a cast. Build time static type checking is not guaranteed.
        /// </summary>
        public static TypedComponents<T> GetMutableTypedComponents<T>(this AbstractEntities entities, string componentName)
        {
            Contract.Requires(entities != null);
            Contract.Requires(entities.GetComponents(componentName) is TypedComponents<T>);
            return (entities as IReadOnlyEntities).GetComponents(componentName) as TypedComponents<T>;
        }

        /// <summary>
        /// Return the int, string NamedEntity tuple corresponding to the given entity name.
        /// The named entity name component will be the same as the given entity name.
        /// </summary>
        public static NamedEntity GetNamedEntity(this IReadOnlyNamedEntities entities, string entityName)
        {
            Contract.Requires(entities != null);
            return new NamedEntity(entities[entityName], entityName);
        }
    }
}
