using System;

namespace TWF
{
    /// <summary>
    /// Factory design pattern for WorldConfig
    /// </summary>
    public class WorldConfigFactory
    {
        public WorldConfig CreateDefaultWorldConfig(Random random)
        {
            var allZones = Zones.AllZones;

            WorldConfig wc = new WorldConfig(
                Zones.AllZones,
                Agents.AllAgents(random),
                ToolBehaviors.Zoners(allZones.Values),
                ToolBrushes.AllToolBrushes);
            return wc;
        }
    }
}
