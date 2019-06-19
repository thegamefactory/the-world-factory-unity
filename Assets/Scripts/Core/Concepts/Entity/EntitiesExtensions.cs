namespace TWF
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    /// <summary>
    /// Static class extending Entities.
    /// </summary>
    public static class EntitiesExtensions
    {
        public static IReadOnlyTypedComponents<T> GetTypedComponents<T>(this IReadOnlyEntities entities, string componentName)
        {
            Contract.Requires(entities != null);
            Contract.Requires(entities.GetComponents(componentName) is IReadOnlyTypedComponents<T>);
            return entities.GetComponents(componentName) as IReadOnlyTypedComponents<T>;
        }

        public static TypedComponents<T> GetMutableTypedComponents<T>(this AbstractEntities entities, string componentName)
        {
            Contract.Requires(entities != null);
            Contract.Requires(entities.GetComponents(componentName) is TypedComponents<T>);
            return (entities as IReadOnlyEntities).GetComponents(componentName) as TypedComponents<T>;
        }

        public static NamedEntity GetNamedEntity(this IReadOnlyNamedEntities entities, string entityName)
        {
            Contract.Requires(entities != null);
            return new NamedEntity(entities[entityName], entityName);
        }

        public static IEnumerable<int> GetMarkedEntities(this IReadOnlyEntities entities, string componentName)
        {
            Contract.Requires(entities != null);
            Contract.Requires(entities.GetComponents(componentName) is TypedComponents<bool>);

            var componentRegistry = entities.GetComponents(componentName) as TypedComponents<bool>;
            return componentRegistry.GetMatchingEntities((v) => v); // returns all entity ids whose boolean component is true
        }

        public static IEnumerable<NamedEntity> GetMarkedNamedEntities(this IReadOnlyNamedEntities entities, string componentName)
        {
            Contract.Requires(entities != null);
            var markedEntities = GetMarkedEntities(entities, componentName);
            var namedEntity = new NamedEntity(-1, string.Empty);

            foreach (var markedEntity in markedEntities)
            {
                namedEntity.SetId(markedEntity);
                namedEntity.SetName(entities[markedEntity]);
                yield return namedEntity;
            }
        }
    }
}
