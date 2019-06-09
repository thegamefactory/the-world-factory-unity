namespace TWF
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// See interface definition.
    /// </summary>
    public class WorldRules : IWorldRules
    {
        public WorldRules(
            Entities zones,
            Entities terrains,
            Dictionary<string, ScheduledAgent> agents,
            Dictionary<string, Func<string, IToolBehavior>> toolBehaviors,
            Dictionary<string, IToolBrush> toolBrushes)
        {
            this.Zones = zones;
            this.Terrains = terrains;
            this.Agents = agents;
            this.ToolBehaviors = toolBehaviors;
            this.ToolBrushes = toolBrushes;
        }

        public Entities Terrains { get; }

        public Entities Zones { get; }

        public Dictionary<string, ScheduledAgent> Agents { get; }

        public Dictionary<string, Func<string, IToolBehavior>> ToolBehaviors { get; }

        public Dictionary<string, IToolBrush> ToolBrushes { get; }

        IReadOnlyEntities IWorldRules.Terrains => this.Terrains;

        IReadOnlyEntities IWorldRules.Zones => this.Zones;

        IReadOnlyDictionary<string, ScheduledAgent> IWorldRules.Agents => this.Agents;

        IReadOnlyDictionary<string, Func<string, IToolBehavior>> IWorldRules.ToolBehaviors => this.ToolBehaviors;

        IReadOnlyDictionary<string, IToolBrush> IWorldRules.ToolBrushes => this.ToolBrushes;
    }
}
