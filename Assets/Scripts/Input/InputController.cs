using UnityEngine;

namespace TWF.Input
{
    public class InputController : MonoBehaviour
    {
        public KeyCode ResidentialModifierKey;
        public KeyCode FarmlandModifierKey;
        public KeyCode RoadModifierKey;

        readonly private Tools tools = new Tools(() => Root.GameService.GetToolApplier(), new MousePositionProvider());

        private void Awake()
        {
            tools.RegisterTool(
                KeyCombination.Builder(ResidentialModifierKey).Build(),
                new Tool("ZoneResidential", ToolBehaviors.ZONER, Zones.RESIDENTIAL, ToolBrushes.Rectangle.Name));

            tools.RegisterTool(
                KeyCombination.Builder(FarmlandModifierKey).Build(),
                new Tool("ZoneFarmland", ToolBehaviors.ZONER, Zones.FARMLAND, ToolBrushes.Rectangle.Name));

            tools.RegisterTool(
                KeyCombination.Builder(RoadModifierKey).Build(),
                new Tool("BuildRoad", ToolBehaviors.ZONER, Zones.ROAD, ToolBrushes.Manatthan.Name));
        }

        void Update()
        {
            tools.Update();
        }
    }
}