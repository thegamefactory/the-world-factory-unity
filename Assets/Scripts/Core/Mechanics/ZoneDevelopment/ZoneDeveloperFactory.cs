namespace TWF
{
    using System;

    /// <summary>
    /// Factory pattern for the ZoneDeveloper.
    /// </summary>
    public class ZoneDeveloperFactory
    {
        private readonly Random random;

        public ZoneDeveloperFactory(Random random)
        {
            this.random = random;
        }

        public ZoneDeveloper CreateZoneDeveloper(double developmentRate)
        {
            return new ZoneDeveloper(() => this.random.NextDouble() < developmentRate);
        }
    }
}
