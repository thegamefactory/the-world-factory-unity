using System.Collections.Generic;

namespace TWF
{
    /// <summary>
    /// Root interface to define a world configuration.
    /// </summary>
    public interface IWorldConfig
    {
        IReadOnlyDictionary<string, Zone> Zones { get; }
        IReadOnlyDictionary<string, ScheduledAgent> Agents { get; }
        IReadOnlyDictionary<string, IToolBehavior> ToolBehaviors { get; }
        IReadOnlyDictionary<string, IToolBrush> ToolBrushes { get; }
    }
}
