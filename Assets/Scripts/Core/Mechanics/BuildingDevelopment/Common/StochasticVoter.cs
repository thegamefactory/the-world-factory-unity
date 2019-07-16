namespace TWF
{
    using System;

    /// <summary>
    /// A voter which votes randomly, to control the development rate.
    /// </summary>
    public class StochasticVoter : IBuildingDevelopmentVoter
    {
        private readonly Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="StochasticVoter"/> class.
        /// </summary>

        /// <param name="random">Provides random value</param>
        public StochasticVoter(Random random)
        {
            this.random = random;
        }

        /// <summary>Gets or sets the development rate between 0 (no development) and 1 (immediate development)</summary>
        public double DevelopmentRate { get; set; }

        public void OnNewWorld(IWorldView worldView)
        {
            // no-op
        }

        public double Vote(Vector pos, int buildingModel)
        {
            return this.random.NextDouble() > this.DevelopmentRate ? 0 : 1;
        }
    }
}
