namespace TWF
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// See interface definition.
    /// </summary>
    public class WorldRules : IWorldRules
    {
        public WorldRules(Random random)
        {
            this.Random = random;
            this.Resources = new NamedEntities(TWF.Resources.EntitiesName);
            this.Zones = new NamedEntities(TWF.Zones.EntitiesName);
            this.Terrains = new NamedEntities(TWF.Terrains.EntitiesName);
            this.BuildingModels = new NamedEntities(TWF.BuildingModels.EntitiesName);
            this.BuildingComponents = new Dictionary<string, IReadOnlyComponents>();
            this.Agents = new Dictionary<string, ScheduledAgent>();
            this.ToolBehaviors = new Dictionary<string, IModifiableToolBehavior>();
            this.ToolBrushes = new Dictionary<string, IToolBrush>();
            this.OnNewWorldListener = (w) => { };
        }

        public NamedEntities Resources { get; }

        public NamedEntities Terrains { get; }

        public NamedEntities Zones { get; }

        public Dictionary<string, ScheduledAgent> Agents { get; }

        public Dictionary<string, IReadOnlyComponents> BuildingComponents { get; }

        public Dictionary<string, IModifiableToolBehavior> ToolBehaviors { get; }

        public Dictionary<string, IToolBrush> ToolBrushes { get; }

        public NamedEntities BuildingModels { get; }

        public Random Random { get; }

        public OnNewWorldListener OnNewWorldListener { get; set; }

        IReadOnlyNamedEntities IWorldRules.Resources => this.Resources;

        IReadOnlyNamedEntities IWorldRules.Terrains => this.Terrains;

        IReadOnlyNamedEntities IWorldRules.Zones => this.Zones;

        IReadOnlyDictionary<string, ScheduledAgent> IWorldRules.Agents => this.Agents;

        IReadOnlyDictionary<string, IModifiableToolBehavior> IWorldRules.ToolBehaviors => this.ToolBehaviors;

        IReadOnlyDictionary<string, IToolBrush> IWorldRules.ToolBrushes => this.ToolBrushes;

        IReadOnlyDictionary<string, IReadOnlyComponents> IWorldRules.BuildingComponents => this.BuildingComponents;

        IReadOnlyNamedEntities IWorldRules.BuildingModels => this.BuildingModels;
    }
}
