using System;
using System.Collections.Generic;
using TWF.Map;

namespace TWF.Tool
{
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
