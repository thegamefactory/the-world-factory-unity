using System.Collections.Generic;
using System.Linq;
using System;

namespace TWF
{
    /// <summary>
    /// A tool brush that yields the positions of all the tiles corresponding with a manatthan road between the start and the end
    /// </summary>
    public class ManatthanToolBrush : IToolBrush
    {
        public string Name => "Manatthan";

        public bool IsValid(IEnumerable<Vector> brushPositions)
        {
            return brushPositions.Count() == 2;
        }

        public IEnumerable<Vector> ComputePositions(IEnumerable<Vector> brushPositions)
        {
            if (0 == brushPositions.Count())
            {
                return brushPositions;
            }

            LinkedList<Vector> result = new LinkedList<Vector>();
            var first = brushPositions.First();
            var second = brushPositions.Last();
            var horizontalStart = first.X < second.X ? first : second;
            var horizontalEnd = first.X < second.X ? second : first;
            var verticalStart = first.Y < second.Y ? first : second;
            var verticalEnd = first.Y < second.Y ? second : first;

            Action<int> processHorizontal = (y) =>
            {
                for (int x = horizontalStart.X; x <= horizontalEnd.X; x++)
                {
                    result.AddLast(new Vector(x, y));
                }
            };
            Action<int> processVertical = (x) =>
            {
                for (int y = verticalStart.Y; y <= verticalEnd.Y; y++)
                {
                    result.AddLast(new Vector(x, y));
                }
            };

            var horizontalDist = horizontalEnd.X - horizontalStart.X;
            var verticalDist = verticalEnd.Y - verticalStart.Y;

            if (verticalDist > horizontalDist)
            {
                processVertical(first.X);
                processHorizontal(second.Y);
            }
            else
            {
                processHorizontal(first.Y);
                processVertical(second.X);
            }

            return result;
        }

        public void AddPosition(LinkedList<Vector> positions, Vector position)
        {
            if (positions.Count >= 2)
            {
                positions.RemoveLast();
            }
            positions.AddLast(position);
        }
    }
}
