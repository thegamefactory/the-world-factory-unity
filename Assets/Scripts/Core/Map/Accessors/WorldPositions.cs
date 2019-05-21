using System.Collections.Generic;

namespace TWF
{
    /// <summary>
    /// A tuple world view / positions.
    /// Having the world view embedded alongside positions enables to map the positions to constructs that are specific to the state.
    /// </summary>
    public class WorldPositions<T>
    {
        public WorldPositions(IMapView<T> mapView, IEnumerable<Vector> positions)
        {
            MapView = mapView;
            Positions = positions;
        }

        public IMapView<T> MapView { get; }
        public IEnumerable<Vector> Positions { get; }

        public T GetMapContent(Vector position)
        {
            return MapView[position];
        }

        public (Vector, T) GetMapContentPositionTuple(Vector position)
        {
            return (position, MapView[position]);
        }
    }
}
