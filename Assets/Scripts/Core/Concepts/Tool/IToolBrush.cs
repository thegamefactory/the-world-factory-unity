using System.Collections.Generic;

namespace TWF
{
    /// <summary>
    /// An interface to define tool brushes.
    /// Tool brushes convert the player input into a list of positions where the tool applies.
    /// For example a rectangle tool brush may convert rectangle corners into all the rectangle tiles.
    /// </summary>
    public interface IToolBrush
    {
        string Name { get; }

        /// <summary>
        /// Returns if the brush can compute the positions with the given collection of input positions.
        /// If it returns false, call to ComputePositions with the same input may fail.
        /// </summary>
        bool IsValid(IEnumerable<Vector> brushPositions);

        /// <summary>
        /// Transforms the brush positions into output positions.
        /// For example, a rectangle brush construct a list of vector corresponding to all the points in the rectangle defined by the first and last element of the given array.
        /// </summary>
        IEnumerable<Vector> ComputePositions(IEnumerable<Vector> brushPositions);

        /// <summary>
        /// Add a position to the linked list of the current positions. 
        /// This enable optimizations, for example a rectangle brush may only care about the first position and the last position.
        /// Therefore adding a position can simply replace the last element of the linked list and the linked list size can be bound to two.
        /// The positions parameter is modified in place
        /// Usage of this method is optional.
        /// </summary>
        void AddPosition(LinkedList<Vector> positions, Vector position);
    }
}
