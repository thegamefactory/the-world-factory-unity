﻿using System;
using System.Collections;
using System.Collections.Generic;
using TWF;
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
                Tile t = Root.GetInstance<GameService>().GetTile(clickedTilePosition.Item1, clickedTilePosition.Item2);
                print(t.Type);
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
