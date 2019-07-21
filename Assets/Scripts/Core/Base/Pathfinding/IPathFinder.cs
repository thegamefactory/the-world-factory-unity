namespace TWF
{
    /// <summary>
    /// A path finder interface.
    /// Finds paths between the origin and the destinaton, specifying a maximum past cost.
    /// The maximum past cost is only meaningful in the context of the connection cost between nodes in the searched graph.
    /// The returned path must be constructed externally.
    /// </summary>
    public interface IPathFinder<TNode>
    {
        bool FindPath(IGraph<TNode> graph, TNode origin, TNode destination, int maxCost, ref Path<TNode> path);
    }
}
