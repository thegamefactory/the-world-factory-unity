namespace TWF
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Static class extending Entities.
    /// </summary>
    public static class EntitiesExtensions
    {
        public static IReadOnlyMarkerComponentRegistry GetMarkerComponents(this IReadOnlyEntities entities, string componentName)
        {
            Contract.Requires(entities != null);
            Contract.Requires(entities.GetComponents(componentName) is IReadOnlyMarkerComponentRegistry);
            return entities.GetComponents(componentName) as IReadOnlyMarkerComponentRegistry;
        }

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

        public static LinkedList<int> GetMarkedEntities(this IReadOnlyEntities entities, string componentName)
        {
            Contract.Requires(entities != null);
            var componentRegistry = entities.GetMarkerComponents(componentName);
            return componentRegistry.MarkedEntities();
        }

        public static IList<NamedEntity> GetMarkedNamedEntities(this IReadOnlyNamedEntities entities, string componentName)
        {
            Contract.Requires(entities != null);
            var markedEntities = GetMarkedEntities(entities, componentName);
            var namedEntites = new List<NamedEntity>(markedEntities.Count);
            int i = 0;

            foreach (var markedEntity in markedEntities)
            {
                namedEntites[i].SetId(markedEntity);
                namedEntites[i].SetName(entities[markedEntity]);
            }

            return namedEntites;
        }
    }
}
