namespace TWF.Input
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class InputController : MonoBehaviour
    {
#pragma warning disable SA1401 // Fields should be private
#pragma warning disable CA1051 // Do not declare visible instance fields
        public KeyCode CommercialModifierKey;
        public KeyCode FarmlandModifierKey;
        public KeyCode ResidentialModifierKey;
        public KeyCode RoadModifierKey;
        public Text DebugText;
#pragma warning restore CA1051 // Do not declare visible instance fields
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
                    KeyCombination.Builder(this.CommercialModifierKey).Build(),
                    zonerBuilder.BuildZoner(Zones.Commercial, "zone", ToolBrushes.Rectangle.Name));

                this.tools.RegisterTool(
                    KeyCombination.Builder(this.FarmlandModifierKey).Build(),
                    zonerBuilder.BuildZoner(Zones.Farmland, "zone", ToolBrushes.Rectangle.Name));

                this.tools.RegisterTool(
                    KeyCombination.Builder(this.ResidentialModifierKey).Build(),
                    zonerBuilder.BuildZoner(Zones.Residential, "zone", ToolBrushes.Rectangle.Name));

                this.tools.RegisterTool(
                    KeyCombination.Builder(this.RoadModifierKey).Build(),
                    zonerBuilder.BuildZoner(Zones.Road, "build", ToolBrushes.Manatthan.Name));
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