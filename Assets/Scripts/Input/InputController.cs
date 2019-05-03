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

    private void Start()
    {
        CurrentModifier = EmptyModifier;
        this.keyEventPublishers.Add(KeyCombinationPublisher.builder(KeyCombination.builder(KeyCode.Mouse0).build())
            .OnActivate(() => AddMousePosition(positions))
            .OnDeactivate(() => ZoneTiles())
            .build());

        this.keyEventPublishers.Add(KeyCombinationPublisher.builder(KeyCombination.builder(ResidentialModifierKey).build())
            .OnActivate(() => CurrentModifier = ResidentialModifier)
            .OnDeactivate(() => CurrentModifier = EmptyModifier)
            .build());
    }

    void Update()
    {
        keyEventPublishers.ForEach((kep) => kep.Enact());
    }

    private void ZoneTiles()
    {
        if (positions.Count == 1 && AddMousePosition(positions)) Root.GameService.ApplyTool(positions, ToolType.ZONER, CurrentModifier);
        positions.Clear();
    }

    private bool AddMousePosition(LinkedList<Vector> vectors)
    {
        try
        {
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
