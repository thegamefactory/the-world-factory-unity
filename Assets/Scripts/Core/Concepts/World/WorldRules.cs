namespace TWF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// See interface definition.
    /// </summary>
    public class WorldRules : IWorldRules
    {
        public WorldRules(
            Random random,
            NamedEntities zones,
            NamedEntities terrains,
            NamedEntities buildingModels,
            ICollection<ScheduledAgent> agents,
            Dictionary<string, Func<string, IToolBehavior>> toolBehaviors,
            ICollection<IToolBrush> toolBrushes)
        {
            this.Random = random;
            this.Zones = zones;
            this.Terrains = terrains;
            this.BuildingModels = buildingModels;
            this.BuildingComponents = new Dictionary<string, IReadOnlyComponents>();
            this.Agents = agents.ToDictionary(s => s.Name);
            this.ToolBehaviors = toolBehaviors;
            this.ToolBrushes = toolBrushes.ToDictionary(tb => tb.Name);
        }

        public NamedEntities Terrains { get; }

        public NamedEntities Zones { get; }

        public Dictionary<string, ScheduledAgent> Agents { get; }

        public Dictionary<string, IReadOnlyComponents> BuildingComponents { get; }

        public Dictionary<string, Func<string, IToolBehavior>> ToolBehaviors { get; }

        public Dictionary<string, IToolBrush> ToolBrushes { get; }

        public NamedEntities BuildingModels { get; }

        public Random Random { get; }

        IReadOnlyNamedEntities IWorldRules.Terrains => this.Terrains;

        IReadOnlyNamedEntities IWorldRules.Zones => this.Zones;

        IReadOnlyDictionary<string, ScheduledAgent> IWorldRules.Agents => this.Agents;

        IReadOnlyDictionary<string, Func<string, IToolBehavior>> IWorldRules.ToolBehaviors => this.ToolBehaviors;

        IReadOnlyDictionary<string, IToolBrush> IWorldRules.ToolBrushes => this.ToolBrushes;

        IReadOnlyDictionary<string, IReadOnlyComponents> IWorldRules.BuildingComponents => this.BuildingComponents;

        IReadOnlyNamedEntities IWorldRules.BuildingModels => this.BuildingModels;
    }
}
