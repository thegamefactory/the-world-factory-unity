namespace TWF
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// A voter which votes randomly, to control the development rate.
    /// </summary>
    public class StochasticVoter : ITileDevelopmentVoter
    {
        private readonly double developmentRate;
        private readonly Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="StochasticVoter"/> class.
        /// </summary>
        /// <param name="developmentRate">A development rate between 0 (no development) and 1 (immediate development)</param>
        /// <param name="random">Provides random value</param>
        public StochasticVoter(double developmentRate, Random random)
        {
            Contract.Requires(developmentRate >= 0.0 && developmentRate <= 1.0);
            Contract.Requires(random != null);

            this.developmentRate = developmentRate;
            this.random = random;
        }

        public void OnNewWorld(IWorldView worldView)
        {
            // no-op
        }

        public double Vote(Vector pos, int buildingModel)
        {
            return this.random.NextDouble() > this.developmentRate ? 0 : 1;
        }
    }
}
