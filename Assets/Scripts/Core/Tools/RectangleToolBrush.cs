using System.Collections.Generic;
using System.Linq;
using System;
using TWF.Map;

namespace TWF.Tool
{
    /// <summary>
    /// A tool brush that yields all rectangle tiles given rectangle corners (inclusive) 
    /// </summary>
    public class RectangleToolBrush : IToolBrush
    {
        public bool IsValid(IEnumerable<Vector> brushPositions)
        {
            return brushPositions.Count() == 2;
        }

        public IEnumerable<Vector> computePositions(IEnumerable<Vector> brushPositions)
        {
            Vector first = brushPositions.First();
            Vector second = brushPositions.Last();
            Vector min = new Vector(Math.Min(first.X, second.X), Math.Min(first.Y, second.Y));
            Vector max = new Vector(Math.Max(first.X, second.X), Math.Max(first.Y, second.Y));
            for (int x = min.X; x <= max.X; x++)
            {
                for (int y = min.Y; y <= max.Y; y++)
                {
                    yield return new Vector(x, y);
                }
            }
        }
    }
}
