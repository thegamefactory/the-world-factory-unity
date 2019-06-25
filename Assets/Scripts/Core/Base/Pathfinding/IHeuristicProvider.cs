namespace TWF
{
    /// <summary>
    /// An algorithm interface to estimate the cost between two nodes for path finding algorithms.
    /// </summary>
    public interface IHeuristicProvider<TNode>
    {
        int EstimateCost(TNode origin, TNode destination);
    }
}
