namespace TWF.Input
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class InputController : MonoBehaviour
    {
#pragma warning disable SA1401 // Fields should be private
        public KeyCode ResidentialModifierKey;
        public KeyCode FarmlandModifierKey;
        public KeyCode RoadModifierKey;
        public Text DebugText;
#pragma warning restore SA1401 // Fields should be private

        private readonly Tools tools = new Tools(() => Root.GameService.GetToolApplier(), new MousePositionProvider());

        public Func<IToolPreviewOutcomeMap> GetToolPreviewOutcomeMapProvider()
        {
            return () => this.tools.CurrentPreviewOutcome ?? ToolPreviewOutcome.EMPTY;
        }

        public Func<Color?> GetToolSuccessColorProvider()
        {
            return () => this.tools.SelectedTool?.PreviewColor;
        }

        public void Awake()
        {
            Root.GameService.OnNewWorldListener += w =>
            {
                var zonerBuilder = new ZonerBuilder(w);

                this.tools.RegisterTool(
                    KeyCombination.Builder(this.ResidentialModifierKey).Build(),
                    zonerBuilder.BuildZoner(Zones.RESIDENTIAL, "zone", ToolBrushes.Rectangle.Name));

                this.tools.RegisterTool(
                    KeyCombination.Builder(this.FarmlandModifierKey).Build(),
                    zonerBuilder.BuildZoner(Zones.FARMLAND, "zone", ToolBrushes.Rectangle.Name));

                this.tools.RegisterTool(
                    KeyCombination.Builder(this.RoadModifierKey).Build(),
                    zonerBuilder.BuildZoner(Zones.ROAD, "build", ToolBrushes.Manatthan.Name));
            };
        }

        public void Update()
        {
            this.tools.Update();
            if (this.DebugText != null)
            {
                this.DebugText.text = this.tools.ToString() + "\n" + "Mouse=" + UnityEngine.Input.GetKey(KeyCode.Mouse0);
            }
        }
    }
}