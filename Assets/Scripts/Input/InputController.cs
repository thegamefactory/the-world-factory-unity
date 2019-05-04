using System;
using System.Collections.Generic;
using TWF;
using TWF.Map;
using TWF.Tool;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public KeyCode ResidentialModifierKey;

    private Modifier EmptyModifier = new Modifier(Tile.TileZone.EMPTY.ToString());
    private Modifier ResidentialModifier = new Modifier(Tile.TileZone.RESIDENTIAL.ToString());
    private Modifier RoadModifier = new Modifier(Tile.TileZone.ROAD.ToString());

    private Modifier CurrentModifier;
    LinkedList<Vector> positions = new LinkedList<Vector>();
    List<KeyCombinationPublisher> keyEventPublishers = new List<KeyCombinationPublisher>();
    ToolBehaviorType toolLock = ToolBehaviorType.NONE;

    private void Start()
    {
        CurrentModifier = EmptyModifier;
        // Zoning
        this.keyEventPublishers.Add(KeyCombinationPublisher.builder(KeyCombination.builder(KeyCode.Mouse0).build())
            .OnActivate(() => AddMousePosition(positions, ToolBehaviorType.ZONER))
            .OnDeactivate(() => ZoneTiles())
            .build());
        // Zoning modifier
        this.keyEventPublishers.Add(KeyCombinationPublisher.builder(KeyCombination.builder(ResidentialModifierKey).build())
            .OnActivate(() => CurrentModifier = ResidentialModifier)
            .OnDeactivate(() => CurrentModifier = EmptyModifier)
            .build());
        // Road building
        this.keyEventPublishers.Add(KeyCombinationPublisher.builder(KeyCombination.builder(KeyCode.Mouse1).build())
            .OnActivate(() => AddMousePosition(positions, ToolBehaviorType.ZONER))
            .OnDeactivate(() => BuildRoad())
            .build());
    }

    void Update()
    {
        keyEventPublishers.ForEach((kep) => kep.Enact());
    }

    private void ZoneTiles()
    {
        if (positions.Count == 1 && AddMousePosition(positions, ToolBehaviorType.ZONER))
        {
            Root.GameService.ApplyTool(ToolBehaviorType.ZONER, CurrentModifier, positions, ToolBrushType.RECTANGLE);
        }
        ResetTool();
    }

    private void BuildRoad()
    {
        if (positions.Count == 1 && AddMousePosition(positions, ToolBehaviorType.ZONER))
        {
            Root.GameService.ApplyTool(ToolBehaviorType.ZONER, RoadModifier, positions, ToolBrushType.MANATTHAN);
        }
        ResetTool();
    }

    private void ResetTool()
    {
        positions.Clear();
        toolLock = ToolBehaviorType.NONE;
    }

    private bool AddMousePosition(LinkedList<Vector> vectors, ToolBehaviorType tool)
    {
        try
        {
            if (toolLock == ToolBehaviorType.NONE)
            {
                toolLock = tool;
            }
            if (toolLock != tool)
            {
                return false;
            }
            Tuple<float, float> clickedTilePosition = CoordinateMapper.ScreenPositionToMeshPosition(Input.mousePosition);
            vectors.AddLast(Root.GameService.GetPosition(clickedTilePosition.Item1, clickedTilePosition.Item2));
            return true;
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
        }

    }
}
