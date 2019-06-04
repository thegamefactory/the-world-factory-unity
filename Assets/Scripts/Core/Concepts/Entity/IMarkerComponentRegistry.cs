using System.Collections.Generic;

namespace TWF
{
    public interface IMarkerComponentRegistry : IComponentRegistry
    {
        bool IsMarked(int entityId);
        LinkedList<int> MarkedEntities();
    }
}
