namespace TWF
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Root interface to define the rules of the world.
    ///
    /// This includes:
    /// - the terrains of the world, as well as their traits (defined as components)
    /// - the zones of the world, as well as their traits (defined as components)
    /// - the background agents that mutate the state of the world
    /// - the tools that the player is allowed to use
    /// </summary>
    public interface IWorldRules
    {
        IReadOnlyEntities Terrains { get; }

        IReadOnlyEntities Zones { get; }

        IReadOnlyDictionary<string, ScheduledAgent> Agents { get; }

        IReadOnlyDictionary<string, Func<string, IToolBehavior>> ToolBehaviors { get; }

        IReadOnlyDictionary<string, IToolBrush> ToolBrushes { get; }
    }
}
