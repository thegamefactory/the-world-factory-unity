namespace TWF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// A datastructure with optimized access (amortized O(1)) for:
    /// - adding an element
    /// - removing an element given its value
    /// - random access
    /// Based on an array and a dictionary cache.
    /// Amortization costs happen for resizing the underlying datastructures.
    /// This class is useful to keep track of a set of elements and picking a random element out of the set.
    /// </summary>
    public class RandomAccessSet<T>
    {
        private readonly IDictionary<T, int> reverseMapping;
        private T[] values;

        public RandomAccessSet(int initialCapacity)
        {
            this.Size = 0;
            this.reverseMapping = new Dictionary<T, int>();
            this.values = new T[initialCapacity];
        }

        public int Size { get; private set; }

        public T this[int index] => this.values[index];

        public void Add(T value)
        {
            Contract.Requires(!this.reverseMapping.ContainsKey(value));

            if (this.Size >= this.values.Length)
            {
                int newSize = this.Size * 2;
                Array.Resize(ref this.values, newSize);
            }

            this.reverseMapping[value] = this.Size;
            this.values[this.Size++] = value;
        }

        public void Remove(T value)
        {
            int index = this.reverseMapping[value];
            T movedValue = this.values[--this.Size];
            this.values[index] = movedValue;
            this.reverseMapping[movedValue] = index;
            this.reverseMapping.Remove(value);
        }
    }
}
