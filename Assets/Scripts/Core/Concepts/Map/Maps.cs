namespace TWF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// A IGenericMap registry.
    /// The world can be thought as containing different maps (layers) superposing each other.
    /// Each layer models the world on a particular dimension.
    /// This class holds all the layers.
    /// </summary>
    public class Maps
    {
        private readonly IDictionary<string, IGenericMap> maps;

        public Maps(Vector size)
        {
            this.Size = size;
            this.maps = new Dictionary<string, IGenericMap>();
        }

        /// <summary>
        /// Gets the size of the map.
        /// </summary>
        /// <return>The size of the map.</return>
        public Vector Size { get; }

        /// <summary>
        /// Gets the size of the map on the X axis.
        /// </summary>
        /// <return>The size of the map on the X axis.</return>
        public int SizeX => this.Size.X;

        /// <summary>
        /// Gets the size of the map on the Y axis.
        /// </summary>
        /// <return>The size of the map on the Y axis.</return>
        public int SizeY => this.Size.Y;

        /// <summary>
        /// Fetches the map corresponding to the given map type.
        /// </summary>
        /// <param name="mapType">Type of the map that is being fetched.</param>
        /// <typeparam name="T">The type of elements contained in the map.</typeparam>
        /// <return>The map corresponding to the given mapType.</return>
        public IMap<T> GetMap<T>(string mapType)
        {
            return (IMap<T>)this.maps[mapType];
        }

        /// <summary>
        /// Registers a new map corresponding to the given map type.
        /// </summary>
        /// <param name="mapType">Type of the map that is being registered.</param>
        /// <param name="map">Actual map that is being registered.</param>
        public void RegisterMap(string mapType, IGenericMap map)
        {
            Contract.Requires(mapType != null);
            Contract.Requires(map != null);

            if (this.maps.ContainsKey(mapType))
            {
                throw new ArgumentException("Duplicate type: " + mapType);
            }

            if (!map.Size.Equals(this.Size))
            {
                throw new ArgumentException("Invalid map size: " + mapType + "(expected: " + this.Size + ", got: " + map.Size + ")");
            }

            this.maps[mapType] = map;
        }
    }
}
