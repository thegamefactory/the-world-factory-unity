namespace TWF
{
    using System.Collections.Generic;

    public class Path<TNode>
    {
        private readonly List<TNode> segments;

        public Path(int initialCapacity)
        {
            this.segments = new List<TNode>(initialCapacity);
        }

        public int Cost { get; set; }

        public int Length { get; private set; }

        public void Reset()
        {
            this.Length = 0;
            this.Cost = 0;
        }

        public void Append(TNode segment)
        {
            if (this.Length >= this.segments.Count)
            {
                this.segments.Add(segment);
            }
            else
            {
                this.segments[this.Length++] = segment;
            }
        }

        public IEnumerable<TNode> GetPath()
        {
            for (int i = this.Length - 1; i >= 0; i--)
            {
                yield return this.segments[i];
            }
        }
    }
}
