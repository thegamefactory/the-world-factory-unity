namespace TWF
{
    using System.Collections.Generic;

    /// <summary>
    /// A minimalistic graph interface.
    /// Able to:
    /// - answer whether a given node is part of the graph or not
    /// - provide connections to neighboring nodes of a given input node and their cost
    /// </summary>
    public interface IGraph<TNode>
    {
        bool IsConnected(TNode node);

        IEnumerable<(TNode, int)> GetWeighedConnections(TNode position);
    }
}
