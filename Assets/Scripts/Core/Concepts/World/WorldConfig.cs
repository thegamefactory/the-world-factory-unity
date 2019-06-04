using System.Collections.Generic;
using System;

namespace TWF
{
    /// <summary>
    /// Root structure to define a world configuration.
    /// </summary>
    public class WorldConfig : IWorldConfig
    {
        public WorldConfig(
            Registry zones,
            Registry terrains,
            Dictionary<string, ScheduledAgent> Agents,
            Dictionary<string, Func<string, IToolBehavior>> ToolBehaviors,
            Dictionary<string, IToolBrush> ToolBrushes)
        {
            this.Zones = zones;
            this.Terrains = terrains;
            this.Agents = Agents;
            this.ToolBehaviors = ToolBehaviors;
            this.ToolBrushes = ToolBrushes;
        }

        public Registry Terrains { get; }
        public Registry Zones { get; }
        public Dictionary<string, ScheduledAgent> Agents { get; }
        public Dictionary<string, Func<string, IToolBehavior>> ToolBehaviors { get; }
        public Dictionary<string, IToolBrush> ToolBrushes { get; }

        IReadOnlyRegistry IWorldConfig.Terrains => Terrains;

        IReadOnlyRegistry IWorldConfig.Zones => Zones;

        IReadOnlyDictionary<string, ScheduledAgent> IWorldConfig.Agents => Agents;

        IReadOnlyDictionary<string, Func<string, IToolBehavior>> IWorldConfig.ToolBehaviors => ToolBehaviors;

        IReadOnlyDictionary<string, IToolBrush> IWorldConfig.ToolBrushes => ToolBrushes;
    }
}
