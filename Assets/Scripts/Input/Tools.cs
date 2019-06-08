using System;
using System.Collections.Generic;
using UnityEngine;

namespace TWF.Input
{
    /// <summary>
    /// A controller for Tools.
    /// 
    /// Tool selection is made via custom key combination injected at construction.
    /// Tool mode is:
    /// - entered when the mouse is clicked and there is a selected tool (the tool is then considered active)
    /// - exited when the escape key is pressed or the tool usage is committed
    /// 
    /// Switching tool cancels and exits tool mode.
    /// </summary>
    public class Tools
    {
        public PreviewOutcome CurrentPreviewOutcome { get; private set; }

        private Tool _SelectedTool;
        private Tool _ActiveTool;
        public Tool SelectedTool
        {
            get
            {
                return this._SelectedTool;
            }
            set
            {
                _SelectedTool = value;
                ResetActiveTool();
            }
        }
        public Tool ActiveTool
        {
            get
            {
                return this._ActiveTool;
            }
            set
            {
                ResetPositions();
                if (null == value || AddCurrentMousePosition())
                {
                    _ActiveTool = value;
                }
            }
        }

        private readonly LinkedList<Vector> positions = new LinkedList<Vector>();

        private readonly Func<IToolApplier> toolApplierProvider;
        private readonly IMousePositionProvider mousePositionProvider;
        private readonly List<KeyCombinationSubject> keyCombinationSubjects = new List<KeyCombinationSubject>();
        private readonly ICollection<Tool> tools = new List<Tool>();

        public Tools(Func<IToolApplier> toolApplierProvider, IMousePositionProvider mousePositionProvider)
            : this(toolApplierProvider, mousePositionProvider, KeyCombination.Builder(KeyCode.Escape).Build(), KeyCombination.Builder(KeyCode.Mouse0).Build())
        {
        }

        public Tools(Func<IToolApplier> toolApplierProvider, IMousePositionProvider mousePositionProvider, IKeyCombination escape, IKeyCombination activator)
        {
            Debug.Assert(null != toolApplierProvider);
            Debug.Assert(null != mousePositionProvider);
            this.toolApplierProvider = toolApplierProvider;
            this.mousePositionProvider = mousePositionProvider;

            keyCombinationSubjects.Add(KeyCombinationSubject.Builder(escape)
                .OnActivate(() => SelectedTool = null)
                .Build());
            keyCombinationSubjects.Add(KeyCombinationSubject.Builder(activator)
                .OnActivate(() => ActiveTool = SelectedTool)
                .OnContinuous(() => ContinueTool())
                .OnDeactivate(() => EnactTool())
                .Build());

        }

        public void Update()
        {
            keyCombinationSubjects.ForEach((kep) => kep.Enact());
        }

        public void RegisterTool(IKeyCombination keyCombination, Tool tool)
        {
            keyCombinationSubjects.Add(KeyCombinationSubject.Builder(keyCombination)
                .OnActivate(() => SelectedTool = tool)
                .Build());
            tools.Add(tool);
        }

        private void ContinueTool()
        {
            DoIfToolIsActive(() =>
            {
                CurrentPreviewOutcome = ActiveTool.Preview(toolApplierProvider(), positions);
            });
        }

        private void EnactTool()
        {
            DoIfToolIsActive(() =>
            {
                ToolOutcome outcome = ActiveTool.Apply(toolApplierProvider(), positions);
                if (ToolOutcome.SUCCESS != outcome)
                {
                    Debug.LogError("Failed to enact " + ActiveTool);
                }
                else
                {
                    Debug.Log("Enacted " + ActiveTool);
                }
            });
            ResetActiveTool();
        }

        private void DoIfToolIsActive(Action action)
        {
            if (null != ActiveTool && ActiveTool == SelectedTool)
            {
                if (AddCurrentMousePosition())
                {
                    action();
                }
            }
        }

        private bool AddCurrentMousePosition()
        {
            Vector? position = mousePositionProvider.GetMousePosition();
            if (null != position)
            {
                positions.AddLast(position.Value);
            }
            return position.HasValue;
        }

        private void ResetActiveTool()
        {
            ResetPositions();
            this.ActiveTool = null;
            this.CurrentPreviewOutcome = null;
        }
        private void ResetPositions()
        {
            positions.Clear();
        }
    }
}
