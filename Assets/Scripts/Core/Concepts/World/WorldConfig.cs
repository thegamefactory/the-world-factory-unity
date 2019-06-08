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
            Entities zones,
            Entities terrains,
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

        public Entities Terrains { get; }
        public Entities Zones { get; }
        public Dictionary<string, ScheduledAgent> Agents { get; }
        public Dictionary<string, Func<string, IToolBehavior>> ToolBehaviors { get; }
        public Dictionary<string, IToolBrush> ToolBrushes { get; }

        IReadOnlyEntities IWorldConfig.Terrains => Terrains;

        IReadOnlyEntities IWorldConfig.Zones => Zones;

        IReadOnlyDictionary<string, ScheduledAgent> IWorldConfig.Agents => Agents;

        IReadOnlyDictionary<string, Func<string, IToolBehavior>> IWorldConfig.ToolBehaviors => ToolBehaviors;

        IReadOnlyDictionary<string, IToolBrush> IWorldConfig.ToolBrushes => ToolBrushes;
    }
}
