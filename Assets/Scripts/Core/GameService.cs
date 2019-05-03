using TWF.Map;
using TWF.Agent;
using TWF.Tool;
using System.Collections.Generic;

namespace TWF
{
    public class GameService : IGameState
    {
        TileMap tileMap;
        EntityMap entityMap;
        IList<IAgent> agents;
        IDictionary<ToolType, ITool> tools;

        public GameService(TileMap tileMap, EntityMap entityMap, IList<IAgent> agents, IDictionary<ToolType, ITool> tools)
        {
            this.tileMap = tileMap;
            this.entityMap = entityMap;
            this.agents = agents;
            this.tools = tools;
        }

        public IEntity GetEntity(int x, int y)
        {
            return entityMap.GetEntity(x, y);
        }

        public Tile GetTile(int x, int y)
        {
            return tileMap.GetTile(x, y);
        }

        public Tile GetTile(float x, float y)
        {
            return tileMap.GetTile(x, y);
        }

        public IEnumerable<(Position, Tile)> GetTiles()
        {
            return tileMap.GetTiles();
        }

        internal void SetEntity(IEntity entity, int x, int y)
        {
            entityMap.SetEntity(entity, x, y);
        }

        public ToolOutcome ApplyTool(LinkedList<Position> positions, ToolType toolType, Modifier modifier)
        {
            return tools[toolType].Apply(positions, tileMap, modifier);
        }

        public ToolOutcome PreviewTool(LinkedList<Position> positions, ToolType toolType, Modifier modifier)
        {
            return tools[toolType].Preview(positions, tileMap, modifier);
        }

        public Position GetPosition(float x, float y)
        {
            return tileMap.GetPosition(x, y);
        }

        public void Tick()
        {
            foreach(IAgent agent in agents)
            {
                agent.execute(this)(this);
            }
        }
    }
}
