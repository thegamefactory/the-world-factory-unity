using UnityEngine;
using System;
using TWF.Graphics;

namespace TWF.Input
{
    public class InputController : MonoBehaviour
    {
        public KeyCode ResidentialModifierKey;
        public KeyCode FarmlandModifierKey;
        public KeyCode RoadModifierKey;

        readonly private Tools tools = new Tools(() => Root.GameService.GetToolApplier(), new MousePositionProvider());

        public Func<IToolPreviewOutcomeMap> GetToolPreviewOutcomeMapProvider()
        {
            return () => tools.CurrentPreviewOutcome ?? ToolPreviewOutcome.EMPTY;
        }

        public Func<Color?> GetToolSuccessColorProvider()
        {
            return () => tools.SelectedTool?.PreviewColor;
        }

        public void Awake()
        {
            Root.GameService.OnNewWorldListener += w =>
            {
                var zonerBuilder = new ZonerBuilder(w);

                tools.RegisterTool(
                    KeyCombination.Builder(ResidentialModifierKey).Build(),
                    zonerBuilder.BuildZoner(Zones.RESIDENTIAL, "zone", ToolBrushes.Rectangle.Name));

                tools.RegisterTool(
                    KeyCombination.Builder(FarmlandModifierKey).Build(),
                    zonerBuilder.BuildZoner(Zones.FARMLAND, "zone", ToolBrushes.Rectangle.Name));

                tools.RegisterTool(
                    KeyCombination.Builder(RoadModifierKey).Build(),
                    zonerBuilder.BuildZoner(Zones.ROAD, "build", ToolBrushes.Manatthan.Name));
            };
        }

        void Update()
        {
            tools.Update();
        }
    }
}