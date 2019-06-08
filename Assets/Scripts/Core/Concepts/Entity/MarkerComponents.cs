using System.Collections.Generic;

namespace TWF
{
    /// <summary>
    /// A simple component that is used to mark entities; this enable a simple component existence check for the entity.
    /// </summary>
    public class MarkerComponent : IReadOnlyMarkerComponentRegistry
    {
        List<bool> markers = new List<bool>();
        public MarkerComponent(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public void MarkEntity(int entityId)
        {
            while (markers.Count <= entityId)
            {
                markers.Add(false);
            }

            markers[entityId] = true;
        }

        public bool IsMarked(int entityId)
        {
            return entityId < markers.Count && markers[entityId];
        }

        public LinkedList<int> MarkedEntities()
        {
            var matchingEntities = new LinkedList<int>();
            int numberOfEntities = markers.Count;
            for (int i = 0; i < numberOfEntities; ++i)
            {
                if (IsMarked(i))
                {
                    matchingEntities.AddLast(i);
                }
            }
            return matchingEntities;
        }
    }
}
