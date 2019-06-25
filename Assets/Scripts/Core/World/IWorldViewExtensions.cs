namespace TWF
{
    using System.Diagnostics.Contracts;

    public static class IWorldViewExtensions
    {
        public static Path<Vector> CreatePath(this IWorldView worldView)
        {
            Contract.Requires(worldView != null);

            return new Path<Vector>(worldView.SizeX + worldView.SizeY);
        }
    }
}
