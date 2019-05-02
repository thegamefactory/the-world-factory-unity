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
        List<IAgent> agents;
        Dictionary<ToolType, ITool> tools;

        public GameService(TileMap tileMap, EntityMap entityMap, List<IAgent> agents)
        {
            this.tileMap = tileMap;
            this.entityMap = entityMap;
            this.agents = agents;
            tools = new Dictionary<ToolType, ITool>();
            tools.Add(ToolType.ZONER, new Zoner());
        }

        public void InitMap(int width, int height)
        {
            tileMap = new TileMap(width, height);
            entityMap = new EntityMap(width, height);
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
