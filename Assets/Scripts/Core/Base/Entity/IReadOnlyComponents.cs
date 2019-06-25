namespace TWF
{
    /// <summary>
    /// Base interface for components that can be attached to entities.
    /// A single implementation of this interface should be attached to the entities.
    /// In other words, we attach one instance to components to the instance of entities.
    /// We do not attach an instance of component to each instance of entity.
    /// That would not make sense considering that entity is just an integer.
    /// </summary>
    public interface IReadOnlyComponents
    {
        /// <summary>
        /// Gets the name of the component. Ex: "resource_need" for "building_model" entities.
        /// </summary>
        string Name { get; }
    }
}
