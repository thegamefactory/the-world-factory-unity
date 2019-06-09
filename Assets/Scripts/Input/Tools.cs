namespace TWF.Input
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

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
        private readonly LinkedList<Vector> positions = new LinkedList<Vector>();

        private readonly Func<IToolApplier> toolApplierProvider;
        private readonly IMousePositionProvider mousePositionProvider;
        private readonly List<KeyCombinationSubject> keyCombinationSubjects = new List<KeyCombinationSubject>();
        private readonly ICollection<ITool> tools = new List<ITool>();

        private ITool selectedTool;
        private ITool activeTool;

        public Tools(Func<IToolApplier> toolApplierProvider, IMousePositionProvider mousePositionProvider)
            : this(toolApplierProvider, mousePositionProvider, KeyCombination.Builder(KeyCode.Escape).Build(), KeyCombination.Builder(KeyCode.Mouse0).Build())
        {
        }

        public Tools(Func<IToolApplier> toolApplierProvider, IMousePositionProvider mousePositionProvider, IKeyCombination escape, IKeyCombination activator)
        {
            Debug.Assert(toolApplierProvider != null);
            Debug.Assert(mousePositionProvider != null);
            this.toolApplierProvider = toolApplierProvider;
            this.mousePositionProvider = mousePositionProvider;

            this.keyCombinationSubjects.Add(KeyCombinationSubject.Builder(escape)
                .OnActivate(() => this.SelectedTool = null)
                .Build());
            this.keyCombinationSubjects.Add(KeyCombinationSubject.Builder(activator)
                .OnActivate(() => this.ActiveTool = this.SelectedTool)
                .OnContinuous(() => this.ContinueTool())
                .OnDeactivate(() => this.EnactTool())
                .Build());
        }

        public ToolPreviewOutcome CurrentPreviewOutcome { get; private set; }

        public ITool SelectedTool
        {
            get
            {
                return this.selectedTool;
            }

            set
            {
                if (value != this.selectedTool)
                {
                    this.selectedTool = value;
                    this.ResetActiveTool();
                }
            }
        }

        public ITool ActiveTool
        {
            get
            {
                return this.activeTool;
            }

            set
            {
                if (value != this.activeTool)
                {
                    this.activeTool = value;
                    this.ResetPositions();
                }
            }
        }

        public void Update()
        {
            this.keyCombinationSubjects.ForEach((kcs) => kcs.Enact());
        }

        public void RegisterTool(IKeyCombination keyCombination, ITool tool)
        {
            this.keyCombinationSubjects.Add(KeyCombinationSubject.Builder(keyCombination)
                .OnActivate(() => this.SelectedTool = tool)
                .Build());
            this.tools.Add(tool);
        }

        public override string ToString()
        {
            return "Tools[selected=" + this.SelectedTool + ", active=" + this.ActiveTool + ", positions" + this.positions.ToReadableString(5) + "]";
        }

        private void ContinueTool()
        {
            this.ProcessMousePosition(() =>
            {
                if (this.AddCurrentMousePosition())
                {
                    this.CurrentPreviewOutcome = this.ActiveTool.Preview(this.toolApplierProvider(), this.positions);
                }
            });
        }

        private void EnactTool()
        {
            this.ProcessMousePosition(() =>
            {
                this.ActiveTool.Apply(this.toolApplierProvider(), this.positions);
            });
            this.ResetActiveTool();
        }

        private void ProcessMousePosition(Action action)
        {
            if (this.ActiveTool != null && this.ActiveTool == this.SelectedTool)
            {
                action();
            }
        }

        private bool AddCurrentMousePosition()
        {
            Vector? position = this.mousePositionProvider.GetMousePosition();
            if (position != null && (this.positions.Count == 0 || this.positions.Last.Value != position))
            {
                this.toolApplierProvider().AddPosition(this.ActiveTool.ToolBrushName, this.positions, position.Value);
            }

            return position.HasValue;
        }

        private void ResetActiveTool()
        {
            this.ResetPositions();
            this.ActiveTool = null;
            this.CurrentPreviewOutcome = null;
        }

        private void ResetPositions()
        {
            this.positions.Clear();
        }
    }
}
