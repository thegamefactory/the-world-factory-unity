using System.Collections.Generic;
using System.Linq;
using System;

namespace TWF
{
    /// <summary>
    /// Factory pattern for the ZoneDeveloper.
    /// </summary>
    public class ZoneDeveloperFactory
    {
        private Random random;

        public ZoneDeveloperFactory(Random random)
        {
            this.random = random;
        }

        public ZoneDeveloper CreateZoneDeveloper(double developmentRate)
        {
            return new ZoneDeveloper(
                    () => random.NextDouble() < developmentRate,
                    random.Next);
        }
    }
}
