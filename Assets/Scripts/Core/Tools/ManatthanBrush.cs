using System.Collections.Generic;
using System.Linq;
using TWF.Map;

namespace TWF.Tool
{
    /// <summary>
    /// A tool brush that yields the positions of all the tiles corresponding with a manatthan road between the start and the end
    /// </summary>
    public class ManatthanBrush : IToolBrush
    {
        public bool IsValid(IEnumerable<Vector> brushPositions)
        {
            return brushPositions.Count() == 2;
        }


        public IEnumerable<Vector> computePositions(IEnumerable<Vector> brushPositions)
        {
            LinkedList<Vector> result = new LinkedList<Vector>();
            var first = brushPositions.First();
            var second = brushPositions.Last();
            var horizontalStart = first.X < second.X ? first : second;
            var horizontalEnd = first.X < second.X ? second : first;
            for (int x = horizontalStart.X; x <= horizontalEnd.X; x++)
            {
                result.AddLast(new Vector(x, first.Y));
            }
            var verticalStart = first.Y < second.Y ? first : second;
            var verticalEnd = first.Y < second.Y ? second : first;
            for (int y = verticalStart.Y; y <= verticalEnd.Y; y++)
            {
                result.AddLast(new Vector(second.X, y));
            }
            return result;
        }
    }
}
