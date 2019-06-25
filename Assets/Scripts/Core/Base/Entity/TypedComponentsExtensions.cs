namespace TWF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Static class extending TypedComponents with a bunch of utility methods.
    /// </summary>
    public static class TypedComponentsExtensions
    {
        /// <summary>
        /// Returns all the components corresponding to the matching predicate.
        /// </summary>
        public static IEnumerable<T> GetMatchingComponents<T>(this IReadOnlyTypedComponents<T> typedComponents, Func<T, bool> predicate)
        {
            Contract.Requires(typedComponents != null);

            IEnumerable<int> entitiesIds = typedComponents.GetMatchingEntities(predicate);
            foreach (int entityId in entitiesIds)
            {
                yield return typedComponents[entityId];
            }
        }

        /// <summary>
        /// Returns true if the component corresponding to the given entity id is true.
        /// </summary>
        public static bool IsMarked(this IReadOnlyTypedComponents<bool> boolComponents, int entityId)
        {
            Contract.Requires(boolComponents != null);

            return boolComponents[entityId];
        }
    }
}
