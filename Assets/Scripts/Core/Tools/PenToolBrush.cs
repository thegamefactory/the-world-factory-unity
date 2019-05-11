using System;
using System.Collections.Generic;
using TWF.Map;

namespace TWF.Tool
{
    /// <summary>
    /// A tool brush that yields the positions given in input.
    /// </summary>
    public class PenToolBrush : IToolBrush
    {
        public bool IsValid(IEnumerable<Vector> brushPositions)
        {
            return true;
        }

        public IEnumerable<Vector> computePositions(IEnumerable<Vector> brushPositions)
        {
            return brushPositions;
        }
    }
}
