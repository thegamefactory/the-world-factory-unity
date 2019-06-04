namespace TWF
{
    public interface IReadOnlyRegistry
    {
        string this[int id] { get; }
        int this[string name] { get; }
        int NumberOfEntities { get; }
        IComponentRegistry GetComponentRegistry(string componentName);
        IMarkerComponentRegistry GetMarkerComponentRegistry(string componentName);
        ITypedComponentRegistry<T> GetTypedComponentRegistry<T>(string componentName);
    }
}
