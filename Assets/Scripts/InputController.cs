using System;
using System.Collections.Generic;
using TWF;
using TWF.Map;
using TWF.Tool;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public KeyCode ResidentialModifierKey;

    private Modifier ResedentialModifier = new Modifier(Tile.TileZone.RESIDENTIAL.ToString());
    private Modifier EmptyModifier = new Modifier(Tile.TileZone.EMPTY.ToString());

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            try
            {
                Tuple<float, float> clickedTilePosition = CoordinateMapper.ScreenPositionToMeshPosition(Input.mousePosition);
                LinkedList<Vector> positions = new LinkedList<Vector>();
                positions.AddLast(Root.GameService.GetPosition(clickedTilePosition.Item1, clickedTilePosition.Item2));
                Root.GameService.ApplyTool(positions, ToolBehaviorType.ZONER, GetModifier());
            }
            catch (ArgumentOutOfRangeException)
            {
                // Did not click an object
            }
        }
    }

    Modifier GetModifier()
    {
        if (Input.GetKey(this.ResidentialModifierKey))
        {
            return ResedentialModifier;
        }
        return EmptyModifier;
    }
}
