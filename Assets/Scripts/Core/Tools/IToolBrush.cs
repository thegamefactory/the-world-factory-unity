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
        bool IsValid(IEnumerable<Vector> brushPositions);
        IEnumerable<Vector> computePositions(IEnumerable<Vector> brushPositions);
    }
}
