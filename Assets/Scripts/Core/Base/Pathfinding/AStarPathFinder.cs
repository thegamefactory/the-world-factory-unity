namespace TWF
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// A path finder implemented using the A* algorithm.
    /// The path finder is stateful and may not be reused across threads.
    /// The path finder is stateful so that finding path doesn't require to allocate memory.
    /// </summary>
    public class AStarPathFinder<TNode> : IPathFinder<TNode>
        where TNode : struct
    {
        private readonly IGraph<TNode> graph;
        private readonly IHeuristicProvider<TNode> heuristicProvider;
        private readonly LinkedList<PathSegment<TNode>> pathSegments;
        private readonly Priority_Queue.FastPriorityQueue<PathSegment<TNode>> openNodes;
        private readonly Dictionary<TNode, (TNode?, int)> originCost;

        public AStarPathFinder(IGraph<TNode> graph, IHeuristicProvider<TNode> heuristicProvider, int maxExplorationSpace)
        {
            this.graph = graph;
            this.heuristicProvider = heuristicProvider;
            this.pathSegments = new LinkedList<PathSegment<TNode>>();

            for (int i = 0; i < maxExplorationSpace; ++i)
            {
                this.pathSegments.AddLast(new PathSegment<TNode>());
            }

            this.openNodes = new Priority_Queue.FastPriorityQueue<PathSegment<TNode>>(this.pathSegments.Count);
            this.originCost = new Dictionary<TNode, (TNode?, int)>(maxExplorationSpace);
        }

        public bool FindPath(TNode origin, TNode destination, int maxCost, ref Path<TNode> path)
        {
            // Fail fast if origin or destination are not connected.
            if (!this.graph.IsConnected(origin) || !this.graph.IsConnected(destination))
            {
                return false;
            }

            Contract.Requires(path != null);

            // the origin cost represents the explored space
            // it is structured as follows:
            // key: the position
            // value: a tuple containing the origin position that leads to this position and the cost
            this.originCost[origin] = (null, 0);

            // enque origin to the space to explore
            this.Enqueue(current: origin, previous: null, priority: 0);

            bool found = false;

            // repeat
            do
            {
                // dequeues the node with the highest priority (0 is the highest priority)
                var currentPathSegment = this.Dequeue();

                TNode current = currentPathSegment.Current;

                // from the current node, lookup in the originCost what was the cost to reach it
                int currentCost = this.originCost[current].Item2;

                // extract from the graph where this node connects to
                var connections = this.graph.GetWeighedConnections(current);

                // for each connection
                foreach (var connection in connections)
                {
                    // get the position it connects to
                    TNode nextNode = connection.Item1;

                    // computes the cost to get to that connection, from the cost to get to the current node plus the cost of the connection
                    int nextCost = currentCost + connection.Item2;

                    // check if we have already visited the position the connection connects to
                    bool alreadyVisited = this.originCost.ContainsKey(nextNode);

                    // if we haven't or if we just found a cheaper to get to that position than previously
                    if (!alreadyVisited || this.originCost[nextNode].Item2 > nextCost)
                    {
                        // tell the origin cost lookup that the cheapest way to get to that node is to use our connection
                        this.originCost[nextNode] = (current, nextCost);
                    }

                    // if we have found our destination, stop
                    if (nextNode.Equals(destination))
                    {
                        found = true;
                        break;
                    }

                    // if it's the first time we reach this node
                    if (!alreadyVisited)
                    {
                        // estimate the remaining cost to get to the destination, using the heuristic and the cost spent so far
                        int estimatedCost = nextCost + this.heuristicProvider.EstimateCost(nextNode, destination);

                        // if this cost is in specified algorithm bounds
                        if (estimatedCost < maxCost)
                        {
                            // enqueue this node as node to explore next, with a priority corresponding of the estimation cost to connect to the destination
                            this.Enqueue(
                                current: nextNode,
                                previous: current,
                                priority: nextCost + this.heuristicProvider.EstimateCost(nextNode, destination));
                        }
                    }
                }

                // continue unless the space is completely explored
            }
            while (!found && this.openNodes.Count > 0);

            if (found)
            {
                // we build the path, from the destination to the origin, following our tracks
                TNode? current = destination;
                path.Reset();

                do
                {
                    path.Append(current.Value);
                    var nextNode = this.originCost[current.Value];
                    current = nextNode.Item1;
                    path.Cost += nextNode.Item2;
                }
                while (current.HasValue);
            }

            this.Reset();
            return found;
        }

        private void Reset()
        {
            foreach (var pathFindingNode in this.openNodes)
            {
                this.FreeNode(pathFindingNode);
            }

            this.openNodes.Clear();
            this.originCost.Clear();
        }

        private PathSegment<TNode> Dequeue()
        {
            var result = this.openNodes.Dequeue();
            this.FreeNode(result);
            return result;
        }

        private void Enqueue(TNode current, TNode? previous, int priority)
        {
            this.openNodes.Enqueue(this.AllocateNode(current, previous), priority);
        }

        private void FreeNode(PathSegment<TNode> pathSegment)
        {
            // we keep the PathFindingNodes in a pool so we need to allocate and free them
            this.pathSegments.AddLast(pathSegment);
        }

        private PathSegment<TNode> AllocateNode(TNode current, TNode? previous)
        {
            // we keep the PathFindingNodes in a pool so we need to allocate and free them
            PathSegment<TNode> segment = this.pathSegments.First.Value;
            segment.Current = current;
            segment.Previous = previous;
            this.pathSegments.RemoveFirst();
            return segment;
        }
    }
}
