namespace TWF
{
    /// <summary>
    /// Base interface for entities that can be extended with new components.
    ///
    /// Note that this interface represent the action of defining a new trait on entities.
    /// Ex: "building models have a resource need trait".
    /// This is different than attaching a specific component to a specific entity.
    /// Ex: "shops need workers".
    /// </summary>
    public interface IExtendableEntities
    {
        void Extend(IReadOnlyComponents component);
    }
}
