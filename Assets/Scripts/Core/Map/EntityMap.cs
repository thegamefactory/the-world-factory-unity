namespace TWF.Map
{
    /// <summary>
    /// A mapper from Position to Entity.
    /// </summary>
    public class EntityMap
    {
        IEntity[,] entities;

        public EntityMap(Vector size)
        {
            entities = new IEntity[size.X, size.Y];
        }

        /// <summary>
        /// Returns the Entity corresponding to the given Position, or null if the map is empty there.
        /// </summary>
        /// <returns>The Entity corresponding to the given Position, or null if the map is empty there.</returns>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        public IEntity GetEntity(int x, int y)
        {
            return entities[x, y];
        }

        /// <summary>
        /// Sets the given Entity at the given Position. If the map is not empty at that position, return the current Entity.
        /// </summary>
        /// <returns>The Entity corresponding to the given Position, or null if the map is empty there.</returns>
        /// <param name="entity">The entity to set.</param>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        public IEntity SetEntity(IEntity entity, int x, int y)
        {
            if (null != entities[x, y])
            {
                return entities[x, y];
            }
            else
            {
                entities[x, y] = entity;
                return null;
            }
        }
    }
}
