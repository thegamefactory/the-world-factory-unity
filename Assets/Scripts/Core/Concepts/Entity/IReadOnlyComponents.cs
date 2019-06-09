namespace TWF
{
    using System.Collections.Generic;

    /// <summary>
    /// Base interface for components that can be attached to entities.
    /// </summary>
    public interface IReadOnlyComponents
    {
        string Name { get; }
    }
}
