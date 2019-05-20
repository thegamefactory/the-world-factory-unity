using System;
using System.Collections.Generic;
using TWF;
using TWF.Map.Tile;
using TWF.Tool;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public KeyCode ResidentialModifierKey;
    public KeyCode FarmlandModifierKey;

    private Modifier EmptyModifier = new Modifier(TileZone.EMPTY.ToString());
    private Modifier ResidentialModifier = new Modifier(TileZone.RESIDENTIAL.ToString());
    private Modifier FarmlandModifier = new Modifier(TileZone.FARMLAND.ToString());
    private Modifier RoadModifier = new Modifier(TileZone.ROAD.ToString());

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
        ZoneModifiers().ForEach(zm =>
        {
            this.keyEventPublishers.Add(KeyCombinationPublisher.builder(KeyCombination.builder(zm.Item2).build())
            .OnActivate(() => CurrentModifier = zm.Item1)
            .OnDeactivate(() => CurrentModifier = EmptyModifier)
            .build());
        });
        // Road building
        this.keyEventPublishers.Add(KeyCombinationPublisher.builder(KeyCombination.builder(KeyCode.Mouse1).build())
            .OnActivate(() => AddMousePosition(positions, ToolBehaviorType.ZONER))
            .OnDeactivate(() => BuildRoad())
            .build());
    }

    private List<(Modifier, KeyCode)> ZoneModifiers()
    {
        return new List<(Modifier, KeyCode)>() { (ResidentialModifier, ResidentialModifierKey), (FarmlandModifier, FarmlandModifierKey) };
    }

    void Update()
    {
        keyEventPublishers.ForEach((kep) => kep.Enact());
    }

    private void ZoneTiles()
    {
        if (positions.Count == 1 && AddMousePosition(positions, ToolBehaviorType.ZONER))
        {
            Modifier currentModifier = CurrentModifier;
            if (ToolOutcome.SUCCESS != Root.GameService.ApplyTool(ToolBehaviorType.ZONER, currentModifier, positions, ToolBrushType.RECTANGLE))
            {
                Debug.LogError("Failed to apply zoner (" + currentModifier + " ) on rectangle " + positions);
            }
            else
            {
                Debug.Log("Zoner " + currentModifier + " applied");
            }
        }
        ResetTool();
    }

    private void BuildRoad()
    {
        if (positions.Count == 1 && AddMousePosition(positions, ToolBehaviorType.ZONER))
        {
            if (ToolOutcome.SUCCESS != Root.GameService.ApplyTool(ToolBehaviorType.ZONER, RoadModifier, positions, ToolBrushType.MANATTHAN))
            {
                Debug.LogError("Failed to apply zoner (" + CurrentModifier + " ) on manatthan " + positions);
            }
            else
            {
                Debug.Log("Road built");
            }
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
            vectors.AddLast(Root.GameService.ConvertPosition(clickedTilePosition.Item1, clickedTilePosition.Item2));
            return true;
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
        }

    }
}
