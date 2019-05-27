using System.Collections.Generic;
using System.Linq;

namespace TWF
{
    /// <summary>
    /// A tool brush that yields all rectangle tiles given rectangle corners (inclusive).
    /// </summary>
    public class RectangleToolBrush : IToolBrush
    {
        public string Name => "Rectangle";

        public bool IsValid(IEnumerable<Vector> brushPositions)
        {
            return brushPositions.Count() == 2;
        }

        public IEnumerable<Vector> computePositions(IEnumerable<Vector> brushPositions)
        {
            return MapPositionsEnumerables.GetPositions(brushPositions.First(), brushPositions.Last());
        }
    }
}
