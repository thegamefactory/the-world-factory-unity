using System.Collections.Generic;
using System.Linq;
using System;
using TWF.State.Map;

namespace TWF.Tool
{
    /// <summary>
    /// A tool brush that yields all rectangle tiles given rectangle corners (inclusive).
    /// </summary>
    public class RectangleToolBrush : IToolBrush
    {
        public bool IsValid(IEnumerable<Vector> brushPositions)
        {
            return brushPositions.Count() == 2;
        }

        public IEnumerable<Vector> computePositions(IEnumerable<Vector> brushPositions)
        {
            return MapTraverser.GetPositions(brushPositions.First(), brushPositions.Last());
        }
    }
}
