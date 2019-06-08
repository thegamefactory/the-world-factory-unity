using System.Collections.Generic;

namespace TWF
{
    /// <summary>
    /// Static class extending Entities.
    /// </summary>
    public static class EntitiesExtensions
    {
        public static IReadOnlyMarkerComponentRegistry GetMarkerComponents(this IReadOnlyEntities entities, string componentName)
        {
            TwfDebug.Assert(entities.GetComponents(componentName) is IReadOnlyMarkerComponentRegistry);
            return entities.GetComponents(componentName) as IReadOnlyMarkerComponentRegistry;
        }
        public static IReadOnlyTypedComponents<T> GetTypedComponents<T>(this IReadOnlyEntities entities, string componentName)
        {
            TwfDebug.Assert(entities.GetComponents(componentName) is IReadOnlyTypedComponents<T>);
            return entities.GetComponents(componentName) as IReadOnlyTypedComponents<T>;
        }

        public static NamedEntity GetNamedEntity(this IReadOnlyEntities entities, string entityName)
        {
            return new NamedEntity(entities[entityName], entityName);
        }

        public static LinkedList<int> GetMarkedEntities(this IReadOnlyEntities entities, string componentName)
        {
            var componentRegistry = entities.GetMarkerComponents(componentName);
            return componentRegistry.MarkedEntities();
        }

        public static IList<NamedEntity> GetMarkedNamedEntities(this IReadOnlyEntities entities, string componentName)
        {
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
