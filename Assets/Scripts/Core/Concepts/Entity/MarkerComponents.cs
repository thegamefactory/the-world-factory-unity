namespace TWF
{
    using System.Collections.Generic;

    /// <summary>
    /// A simple component that is used to mark entities; this enable a simple component existence check for the entity.
    /// </summary>
    public class MarkerComponent : IReadOnlyMarkerComponentRegistry
    {
        private readonly List<bool> markers = new List<bool>();

        public MarkerComponent(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        public void MarkEntity(int entityId)
        {
            while (this.markers.Count <= entityId)
            {
                this.markers.Add(false);
            }

            this.markers[entityId] = true;
        }

        public bool IsMarked(int entityId)
        {
            return entityId < this.markers.Count && this.markers[entityId];
        }

        public LinkedList<int> MarkedEntities()
        {
            var matchingEntities = new LinkedList<int>();
            int numberOfEntities = this.markers.Count;
            for (int i = 0; i < numberOfEntities; ++i)
            {
                if (this.IsMarked(i))
                {
                    matchingEntities.AddLast(i);
                }
            }

            return matchingEntities;
        }
    }
}
