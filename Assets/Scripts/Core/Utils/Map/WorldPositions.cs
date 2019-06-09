﻿namespace TWF
{
    using System.Collections.Generic;

    /// <summary>
    /// A tuple map view / positions.
    /// Having the world view embedded alongside positions enables to map the positions to constructs that are specific to the state.
    /// </summary>
    /// <typeparam name="T">The type of elements contained in the map view.</typeparam>
    public class WorldPositions<T>
    {
        public WorldPositions(IMapView<T> mapView, IEnumerable<Vector> positions)
        {
            this.MapView = mapView;
            this.Positions = positions;
        }

        public IMapView<T> MapView { get; }

        public IEnumerable<Vector> Positions { get; }

        public T GetMapContent(Vector position)
        {
            return this.MapView[position];
        }

        public (Vector, T) GetMapContentPositionTuple(Vector position)
        {
            return (position, this.MapView[position]);
        }
    }
}
