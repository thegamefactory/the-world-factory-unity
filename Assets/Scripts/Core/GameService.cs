using TWF.Map;
using TWF.Agent;
using System.Collections.Generic;

namespace TWF
{
    public class GameService : IGameState
    {
        TileMap tileMap;
        EntityMap entityMap;
        List<IAgent> agents;

        public GameService(TileMap tileMap, EntityMap entityMap, List<IAgent> agents)
        {
            this.tileMap = tileMap;
            this.entityMap = entityMap;
            this.agents = agents;
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

        public void SetTileType(int x, int y, Tile.TileType type)
        {
            tileMap.GetTile(x, y).Type = type;
        }

        public void SetTileType(float x, float y, Tile.TileType type)
        {
            tileMap.GetTile(x, y).Type = type;
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
