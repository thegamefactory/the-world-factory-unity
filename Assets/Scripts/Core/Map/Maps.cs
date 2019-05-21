using System;
using System.Collections.Generic;

namespace TWF
{
    /// <summary>
    /// A IGenericMap registry.
    /// The world can be thought as containing different maps (layers) superposing each other.
    /// Each layer models the world on a particular dimension.
    /// This class holds all the layers.
    /// </summary>
    public class Maps
    {
        IDictionary<MapType, IGenericMap> maps;

        public Maps(Vector size)
        {
            Size = size;
            maps = new Dictionary<MapType, IGenericMap>();
        }

        /// <summary>
        /// Fetches the map corresponding to the given map type.
        /// </summary>
        /// <param name="mapType">Type of the map that is being fetched.</param>
        /// <return>The map corresponding to the given mapType.</return>
        public IMap<T> GetMap<T>(MapType mapType)
        {
            return (IMap<T>)this.maps[mapType];
        }

        /// <summary>
        /// Registers a new map corresponding to the given map type.
        /// </summary>
        /// <param name="mapType">Type of the map that is being registered.</param>
        /// <param name="map">Actual map that is being registered.</param>
        public void RegisterMap(MapType mapType, IGenericMap map)
        {
            if (maps.ContainsKey(mapType))
            {
                throw new ArgumentException("Duplicate type: " + mapType);
            }
            if (!map.Size.Equals(Size))
            {
                throw new ArgumentException("Invalid map size: " + mapType + "(expected: " + Size + ", got: " + map.Size + ")");
            }
            this.maps[mapType] = map;
        }

        /// <summary>
        /// Returns the size of the map.
        /// </summary>
        /// <return>The size of the map.</return>
        public Vector Size { get; }

        /// <summary>
        /// Returns the size of the map on the X axis.
        /// </summary>
        /// <return>The size of the map on the X axis.</return>
        public int SizeX => Size.X;

        /// <summary>
        /// Returns the size of the map on the Y axis.
        /// </summary>
        /// <return>The size of the map on the Y axis.</return>
        public int SizeY => Size.Y;
    }
}
