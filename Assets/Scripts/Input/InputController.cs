using System;
using System.Collections.Generic;
using TWF;
using TWF.Map;
using TWF.Tool;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public KeyCode ResidentialModifierKey;

    private Modifier ResidentialModifier = new Modifier(Tile.Zone.RESIDENTIAL.ToString());
    private Modifier EmptyModifier = new Modifier(Tile.Zone.EMPTY.ToString());
    private Modifier CurrentModifier;
    LinkedList<Vector> positions = new LinkedList<Vector>();
    List<KeyCombinationPublisher> keyEventPublishers = new List<KeyCombinationPublisher>();
    ToolType toolLock = ToolType.NONE;

    private void Start()
    {
        CurrentModifier = EmptyModifier;
        // Zoning
        this.keyEventPublishers.Add(KeyCombinationPublisher.builder(KeyCombination.builder(KeyCode.Mouse0).build())
            .OnActivate(() => AddMousePosition(positions, ToolType.ZONER))
            .OnDeactivate(() => ZoneTiles())
            .build());
        // Zoning modifier
        this.keyEventPublishers.Add(KeyCombinationPublisher.builder(KeyCombination.builder(ResidentialModifierKey).build())
            .OnActivate(() => CurrentModifier = ResidentialModifier)
            .OnDeactivate(() => CurrentModifier = EmptyModifier)
            .build());
        // Road building
        this.keyEventPublishers.Add(KeyCombinationPublisher.builder(KeyCombination.builder(KeyCode.Mouse1).build())
            .OnActivate(() => AddMousePosition(positions, ToolType.ROAD_BUILDER))
            .OnDeactivate(() => BuildRoad())
            .build());
    }

    void Update()
    {
        keyEventPublishers.ForEach((kep) => kep.Enact());
    }

    private void ZoneTiles()
    {
        if (positions.Count == 1 && AddMousePosition(positions, ToolType.ZONER))
        {
            Root.GameService.ApplyTool(positions, ToolType.ZONER, CurrentModifier);
        }
        ResetTool();
    }

    private void BuildRoad()
    {
        if (positions.Count == 1 && AddMousePosition(positions, ToolType.ROAD_BUILDER))
        {
            Root.GameService.ApplyTool(positions, ToolType.ROAD_BUILDER, null);
        }
        ResetTool();
    }

    private void ResetTool()
    {
        positions.Clear();
        toolLock = ToolType.NONE;
    }

    private bool AddMousePosition(LinkedList<Vector> vectors, ToolType tool)
    {
        try
        {
            if (toolLock == ToolType.NONE)
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
