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
        public ToolPreviewOutcome CurrentPreviewOutcome { get; private set; }

        private ITool _SelectedTool;
        private ITool _ActiveTool;
        public ITool SelectedTool
        {
            get
            {
                return this._SelectedTool;
            }
            set
            {
                if (value != _SelectedTool)
                {
                    _SelectedTool = value;
                    ResetActiveTool();
                }
            }
        }
        public ITool ActiveTool
        {
            get
            {
                return this._ActiveTool;
            }
            set
            {
                if (value != _ActiveTool)
                {
                    _ActiveTool = value;
                    ResetPositions();
                }
            }
        }

        private readonly LinkedList<Vector> positions = new LinkedList<Vector>();

        private readonly Func<IToolApplier> toolApplierProvider;
        private readonly IMousePositionProvider mousePositionProvider;
        private readonly List<KeyCombinationSubject> keyCombinationSubjects = new List<KeyCombinationSubject>();
        private readonly ICollection<ITool> tools = new List<ITool>();

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
            keyCombinationSubjects.ForEach((kcs) => kcs.Enact());
        }

        public void RegisterTool(IKeyCombination keyCombination, ITool tool)
        {
            keyCombinationSubjects.Add(KeyCombinationSubject.Builder(keyCombination)
                .OnActivate(() => SelectedTool = tool)
                .Build());
            tools.Add(tool);
        }

        public override string ToString()
        {
            return "Tools[selected=" + SelectedTool + ", active=" + ActiveTool + ", positions" + positions.ToReadableString(5) + "]";
        }

        private void ContinueTool()
        {
            ProcessMousePosition(() =>
            {
                if (AddCurrentMousePosition())
                {
                    CurrentPreviewOutcome = ActiveTool.Preview(toolApplierProvider(), positions);
                }
            });
        }

        private void EnactTool()
        {
            ProcessMousePosition(() =>
            {
                ActiveTool.Apply(toolApplierProvider(), positions);
            });
            ResetActiveTool();
        }

        private void ProcessMousePosition(Action action)
        {
            if (null != ActiveTool && ActiveTool == SelectedTool)
            {
                action();
            }
        }

        private bool AddCurrentMousePosition()
        {
            Vector? position = mousePositionProvider.GetMousePosition();
            if (null != position && (positions.Count == 0 || positions.Last.Value != position))
            {
                toolApplierProvider().AddPosition(ActiveTool.ToolBrushName, positions, position.Value);
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
