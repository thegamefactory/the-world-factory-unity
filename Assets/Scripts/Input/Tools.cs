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
        private Tool currentTool;
        private Tool activeTool;
        private PreviewOutcome currentPreviewOutcome;
        private LinkedList<Vector> positions = new LinkedList<Vector>();

        private Func<IToolApplier> toolApplierProvider;
        private List<KeyCombinationSubject> keyCombinationSubjects = new List<KeyCombinationSubject>();
        private ICollection<Tool> tools = new List<Tool>();

        public Tools(Func<IToolApplier> toolApplierProvider)
        {
            this.toolApplierProvider = toolApplierProvider;
        }

        public Tools()
        {
            keyCombinationSubjects.Add(KeyCombinationSubject.builder(KeyCombination.builder(KeyCode.Escape).build())
                .OnActivate(() => SwitchTool(null))
                .build());
            keyCombinationSubjects.Add(KeyCombinationSubject.builder(KeyCombination.builder(KeyCode.Mouse0).build())
                .OnActivate(() => ActivateTool())
                .OnContinuous(() => ContinueTool())
                .OnDeactivate(() => EnactTool())
                .build());
        }

        public void Update()
        {
            keyCombinationSubjects.ForEach((kep) => kep.Enact());
        }

        public void RegisterTool(KeyCombination keyCombination, Tool tool)
        {
            keyCombinationSubjects.Add(KeyCombinationSubject.builder(keyCombination)
                .OnActivate(() => SwitchTool(tool))
                .build());
            tools.Add(tool);
        }

        public void SwitchTool(Tool newTool)
        {
            currentTool = newTool;
            ResetActiveTool();
        }

        public void ActivateTool()
        {
            ResetPositions();
            if (AddCurrentMousePosition())
            {
                activeTool = currentTool;
            }
        }

        public void ContinueTool()
        {
            doIfToolIsActive(() =>
            {
                currentPreviewOutcome = activeTool.Preview(toolApplierProvider(), positions);
            });
        }

        public void EnactTool()
        {
            doIfToolIsActive(() =>
            {
                ToolOutcome outcome = activeTool.Apply(toolApplierProvider(), positions);
                if (ToolOutcome.SUCCESS != outcome)
                {
                    Debug.LogError("Failed to enact " + activeTool);
                }
                else
                {
                    Debug.Log("Enacted " + activeTool);
                }
            });
            ResetActiveTool();
        }

        private void doIfToolIsActive(Action action)
        {
            if (null != activeTool && activeTool == currentTool)
            {
                if (AddCurrentMousePosition())
                {
                    action();
                }
            }
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

        private void ResetActiveTool()
        {
            ResetPositions();
            this.activeTool = null;
            this.currentPreviewOutcome = null;
        }
        private void ResetPositions()
        {
            positions.Clear();
        }
    }
}
