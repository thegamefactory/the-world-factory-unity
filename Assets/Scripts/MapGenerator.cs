using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TWF;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public Color evenColor;
    public Color oddColor;

    private GameService gameService;

    void Start()
    {
        gameService = new GameService();
        gameService.InitMap(mapWidth, mapHeight);
        Generate();
    }
    
    void Update()
    {

    }

    void Generate()
    {
        float offsetX = (mapWidth - 1) / 2.0f;
        float offsetY = (mapHeight - 1) / 2.0f;
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Quad);
                plane.gameObject.transform.position = new Vector3(x - offsetX, y - offsetY, 0);
                Material tileMaterial = new Material(Shader.Find("Unlit/Color"));
                gameService.SetTileType(x, y, (x + y) % 2 == 0 ? Tile.TileType.EMPTY : Tile.TileType.RESIDENTIAL);
                tileMaterial.color = gameService.GetTile(x, y).Type == Tile.TileType.RESIDENTIAL ? evenColor : oddColor;
                plane.gameObject.GetComponent<Renderer>().material = tileMaterial;
            }
        }
    }
}
