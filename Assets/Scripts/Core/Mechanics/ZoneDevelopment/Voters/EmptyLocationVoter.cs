namespace TWF
{
    using System.Diagnostics.Contracts;

    public class EmptyLocationVoter : IZoneDevelopmentVoter
    {
        private static EmptyLocationVoter instance;

        private IMapView<int> buildings;

        private EmptyLocationVoter()
        {
        }

        public static EmptyLocationVoter GetInstance()
        {
            if (instance == null)
            {
                instance = new EmptyLocationVoter();
            }

            return instance;
        }

        public void OnNewWorld(IWorldView worldView)
        {
            Contract.Requires(worldView != null);

            this.buildings = worldView.GetMapView<int>(MapTypes.Building);
        }

        public double Vote(Vector pos)
        {
            return this.buildings[pos] == MapTypes.NoBuilding ? 1.0 : 0.0;
        }
    }
}
