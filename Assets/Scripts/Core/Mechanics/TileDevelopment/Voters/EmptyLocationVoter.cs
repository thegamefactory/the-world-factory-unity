﻿namespace TWF
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// A voter which is only inclined for empty locations.
    /// </summary>
    public class EmptyLocationVoter : ITileDevelopmentVoter
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

        public double Vote(Vector pos, int buildingModel)
        {
            return this.buildings[pos] == MapTypes.NoBuilding ? 1.0 : 0.0;
        }
    }
}
