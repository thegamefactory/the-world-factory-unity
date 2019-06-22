namespace TWF
{
    using System.Collections.Generic;

    /// <summary>
    /// A registry of entities.
    ///
    /// The interface doesn't specify how the entity itself should be modeled.
    /// It just specifies that components can be attached to the entities.
    /// Note that components are attached to the registry itself, not to individual entities.
    /// </summary>
    public interface IReadOnlyEntities
    {
        string Name { get; }

        int NumberOfEntities { get; }

        IReadOnlyComponents GetComponents(string componentName);
    }
}
