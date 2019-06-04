using System.Collections.Generic;

namespace TWF
{
    public static class RegistryExtensions
    {
        public static NamedEntity GetNamedEntity(this IReadOnlyRegistry registry, string entityName)
        {
            return new NamedEntity(registry[entityName], entityName);
        }

        public static LinkedList<int> GetMarkedEntities(this IReadOnlyRegistry registry, string componentName)
        {
            var componentRegistry = registry.GetMarkerComponentRegistry(componentName);
            return componentRegistry.MarkedEntities();
        }

        public static IList<NamedEntity> GetMarkedNamedEntities(this IReadOnlyRegistry registry, string componentName)
        {
            var markedEntities = GetMarkedEntities(registry, componentName);
            var namedEntites = new List<NamedEntity>(markedEntities.Count);
            int i = 0;

            foreach (var markedEntity in markedEntities)
            {
                namedEntites[i].SetId(markedEntity);
                namedEntites[i].SetName(registry[markedEntity]);
            }

            return namedEntites;
        }
    }
}
