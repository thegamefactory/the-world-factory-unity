using System.Collections.Generic;
using TWF.Map;

namespace TWF.Tool
{
    public interface ITool
    {
        ToolType ToolType { get; }

        ToolOutcome Apply(LinkedList<Position> inputPositions, TileMap map, Modifier modifier);

        ToolOutcome Preview(LinkedList<Position> inputPositions, TileMap map, Modifier modifier);

        ISet<Modifier> GetModifiers();
    }
}
