using System;
using TWF;
using TWF.Map;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public KeyCode ResidentialModifier;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            try
            {
                Tuple<float, float> clickedTilePosition = CoordinateMapper.ScreenPositionToMeshPosition(Input.mousePosition);
                Root.GameService.SetTileType(clickedTilePosition.Item1, clickedTilePosition.Item2, GetModifiedTileType());
                print(Root.GameService.GetTile(clickedTilePosition.Item1, clickedTilePosition.Item2).Type);
            } catch(ArgumentOutOfRangeException)
            {
                // Did not click an object
            }
        }
    }

    Tile.TileType GetModifiedTileType()
    {
        if (Input.GetKey(this.ResidentialModifier))
        {
            return Tile.TileType.RESIDENTIAL;
        }
        return Tile.TileType.EMPTY;
    }
}
