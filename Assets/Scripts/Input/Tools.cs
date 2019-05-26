using System;
using System.Collections.Generic;
using UnityEngine;

namespace TWF.Input
{
    /// <summary>
    /// A controller for Tools.
    /// 
    /// Define a tool mode that is 
    /// - entered when the mouse is clicked and there is an active tool
    /// - exited when the escape key is pressed or the tool usage is committed
    /// 
    /// Switching tool cancels the current tool usage
    /// </summary>
    public class Tools
    {
        private ToolName currentTool;
        private ToolName activeTool;
        private LinkedList<Vector> positions = new LinkedList<Vector>();
        private List<KeyCombinationSubject> keyCombinationSubjects = new List<KeyCombinationSubject>();
        private IDictionary<ToolName, Func<ICollection<Vector>, ToolOutcome>> tools = new Dictionary<ToolName, Func<ICollection<Vector>, ToolOutcome>>();

        public Tools()
        {
            keyCombinationSubjects.Add(KeyCombinationSubject.builder(KeyCombination.builder(KeyCode.Escape).build())
                .OnActivate(() => SwitchTool(null))
                .build());
            keyCombinationSubjects.Add(KeyCombinationSubject.builder(KeyCombination.builder(KeyCode.Mouse0).build())
                .OnActivate(() => BeginTool())
                .OnDeactivate(() => EndTool())
                .build());
        }

        public void Update()
        {
            keyCombinationSubjects.ForEach((kep) => kep.Enact());
        }

        public void RegisterTool(KeyCombination keyCombination, ToolName toolName, Func<ICollection<Vector>, ToolOutcome> onCommit)
        {
            keyCombinationSubjects.Add(KeyCombinationSubject.builder(keyCombination)
                .OnActivate(() => SwitchTool(toolName))
                .build());
            tools.Add(toolName, onCommit);
        }

        public void SwitchTool(ToolName newTool)
        {
            activeTool = null;
            ResetPositions();
            currentTool = newTool;
        }

        public void BeginTool()
        {
            activeTool = currentTool;
            ResetPositions();
            AddCurrentMousePosition();
        }

        public void EndTool()
        {
            if (null != activeTool && activeTool == currentTool)
            {
                if (AddCurrentMousePosition())
                {
                    ToolOutcome outcome = tools[activeTool](positions);
                    if (ToolOutcome.SUCCESS != outcome)
                    {
                        Debug.LogError("Failed to enact " + activeTool);
                    }
                    else
                    {
                        Debug.Log("Enacted " + activeTool);
                    }
                }
            }
            activeTool = null;
            ResetPositions();
        }

        public bool AddCurrentMousePosition()
        {
            try
            {
                Tuple<float, float> clickedPosition = CoordinateMapper.ScreenPositionToMeshPosition(UnityEngine.Input.mousePosition);
                positions.AddLast(Root.GameService.ConvertPosition(clickedPosition.Item1, clickedPosition.Item2));
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }

        public void ResetPositions()
        {
            positions.Clear();
        }
    }
}
