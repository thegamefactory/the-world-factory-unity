using System.Collections.Generic;

namespace TWF
{
    /// <summary>
    /// A tool brush that yields the positions given in input.
    /// </summary>
    public class PenToolBrush : IToolBrush
    {
        public string Name => "Pen";

        public bool IsValid(IEnumerable<Vector> brushPositions)
        {
            return true;
        }

        public IEnumerable<Vector> ComputePositions(IEnumerable<Vector> brushPositions)
        {
            return brushPositions;
        }

        public void AddPosition(LinkedList<Vector> positions, Vector position)
        {
            positions.AddLast(position);
        }
    }
}
