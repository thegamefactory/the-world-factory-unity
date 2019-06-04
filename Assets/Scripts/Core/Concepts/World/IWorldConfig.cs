using System.Collections.Generic;
using System;

namespace TWF
{
    /// <summary>
    /// Root interface to define a world configuration.
    /// </summary>
    public interface IWorldConfig
    {
        IReadOnlyRegistry Terrains { get; }
        IReadOnlyRegistry Zones { get; }
        IReadOnlyDictionary<string, ScheduledAgent> Agents { get; }
        IReadOnlyDictionary<string, Func<string, IToolBehavior>> ToolBehaviors { get; }
        IReadOnlyDictionary<string, IToolBrush> ToolBrushes { get; }
    }
}
