using System.Collections.Generic;

namespace TWF
{
    /// <summary>
    /// Interface for richly typed components that can be attached to entities.
    /// </summary>
    public interface IReadOnlyTypedComponents<T> : IReadOnlyComponents
    {
        T GetComponent(int entityId);
    }
}
