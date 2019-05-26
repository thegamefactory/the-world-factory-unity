using System.Collections.Generic;

namespace TWF
{
    /// <summary>
    /// Root structure to define a world configuration.
    /// </summary>
    public class WorldConfig : IWorldConfig
    {
        public WorldConfig(Dictionary<string, Zone> Zones,
            Dictionary<string, ScheduledAgent> Agents,
            Dictionary<string, IToolBehavior> ToolBehaviors,
            Dictionary<string, IToolBrush> ToolBrushes)
        {
            this.Zones = Zones;
            this.Agents = Agents;
            this.ToolBehaviors = ToolBehaviors;
            this.ToolBrushes = ToolBrushes;
        }

        public IReadOnlyDictionary<string, Zone> Zones { get; }
        public IReadOnlyDictionary<string, ScheduledAgent> Agents { get; }
        public IReadOnlyDictionary<string, IToolBehavior> ToolBehaviors { get; }
        public IReadOnlyDictionary<string, IToolBrush> ToolBrushes { get; }
    }
}
