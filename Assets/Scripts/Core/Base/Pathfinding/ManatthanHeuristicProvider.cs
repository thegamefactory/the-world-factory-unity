namespace TWF
{
    using System;

    public class ManatthanHeuristicProvider : IHeuristicProvider<Vector>
    {
        public int EstimateCost(Vector origin, Vector destination)
        {
            return Math.Abs(origin.X - destination.X) + Math.Abs(origin.Y - destination.Y);
        }
    }
}
