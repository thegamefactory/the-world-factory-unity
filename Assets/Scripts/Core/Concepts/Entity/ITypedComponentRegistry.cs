using System.Collections.Generic;

namespace TWF
{
    public interface ITypedComponentRegistry<T> : IComponentRegistry
    {
        T GetComponent(int entityId);
    }
}
