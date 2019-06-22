namespace TWF
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for richly typed components that can be attached to entities.
    /// </summary>
    /// <typeparam name="T">The type of the component.</param>
    public interface IReadOnlyTypedComponents<T> : IReadOnlyComponents
    {
        T this[int entityId] { get; }

        IEnumerable<(int, T)> GetEntityComponentPairs();

        IEnumerable<int> GetMatchingEntities(Func<T, bool> predicate);
    }
}
