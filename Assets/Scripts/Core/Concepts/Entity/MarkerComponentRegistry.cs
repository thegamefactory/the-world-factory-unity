using System.Collections.Generic;

namespace TWF
{
    public class MarkerComponent : IMarkerComponentRegistry
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
