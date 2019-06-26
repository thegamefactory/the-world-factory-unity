namespace TWF
{
    using Priority_Queue;

    /// <summary>
    /// A path segment to use with the Priority_Queue library.
    /// </summary>
    internal class PathSegment<TNode> : FastPriorityQueueNode
        where TNode : struct
    {
        public TNode Current { get; set; }

        public TNode? Previous { get; set; }
    }
}
