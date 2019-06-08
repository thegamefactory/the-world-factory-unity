using System.Collections.Generic;
using System;

namespace TWF
{
    /// <summary>
    /// See interface definition.
    /// </summary>
    public class WorldRules : IWorldRules
    {
        public WorldRules(
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

        IReadOnlyEntities IWorldRules.Terrains => Terrains;

        IReadOnlyEntities IWorldRules.Zones => Zones;

        IReadOnlyDictionary<string, ScheduledAgent> IWorldRules.Agents => Agents;

        IReadOnlyDictionary<string, Func<string, IToolBehavior>> IWorldRules.ToolBehaviors => ToolBehaviors;

        IReadOnlyDictionary<string, IToolBrush> IWorldRules.ToolBrushes => ToolBrushes;
    }
}
