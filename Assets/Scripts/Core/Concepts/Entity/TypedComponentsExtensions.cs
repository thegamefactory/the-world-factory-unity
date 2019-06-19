namespace TWF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    public static class TypedComponentsExtensions
    {
        public static IEnumerable<T> GetMatchingComponents<T>(this IReadOnlyTypedComponents<T> typedComponents, Func<T, bool> predicate)
        {
            Contract.Requires(typedComponents != null);

            IEnumerable<int> entitiesIds = typedComponents.GetMatchingEntities(predicate);
            foreach (int entityId in entitiesIds)
            {
                yield return typedComponents.GetComponent(entityId);
            }
        }

        public static bool IsMarked(this IReadOnlyTypedComponents<bool> boolComponents, int entityId)
        {
            Contract.Requires(boolComponents != null);

            return boolComponents.GetComponent(entityId);
        }
    }
}
