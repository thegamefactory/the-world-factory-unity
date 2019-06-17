﻿namespace TWF
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
            this.Zones = new NamedEntities(TWF.Zones.EntitiesName);
            this.Terrains = new NamedEntities(TWF.Terrains.EntitiesName);
            this.BuildingModels = new NamedEntities(TWF.BuildingModels.EntitiesName);
            this.BuildingComponents = new Dictionary<string, IReadOnlyComponents>();
            this.Agents = new Dictionary<string, ScheduledAgent>();
            this.ToolBehaviors = new Dictionary<string, Func<string, IToolBehavior>>();
            this.ToolBrushes = new Dictionary<string, IToolBrush>();
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
