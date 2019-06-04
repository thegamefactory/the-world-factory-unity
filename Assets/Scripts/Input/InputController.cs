using UnityEngine;

namespace TWF.Input
{
    public class InputController : MonoBehaviour
    {
        public KeyCode ResidentialModifierKey;
        public KeyCode FarmlandModifierKey;
        public KeyCode RoadModifierKey;

        readonly private Tools tools = new Tools();

        private void Start()
        {
            tools.RegisterTool(
                KeyCombination.builder(ResidentialModifierKey).build(),
                new Tool("ZoneResidential", ToolBehaviors.ZONER, Zones.RESIDENTIAL, ToolBrushes.Rectangle.Name));

            tools.RegisterTool(
                KeyCombination.builder(FarmlandModifierKey).build(),
                new Tool("ZoneFarmland", ToolBehaviors.ZONER, Zones.FARMLAND, ToolBrushes.Rectangle.Name));

            tools.RegisterTool(
                KeyCombination.builder(RoadModifierKey).build(),
                new Tool("BuildRoad", ToolBehaviors.ZONER, Zones.ROAD, ToolBrushes.Manatthan.Name));
        }

        void Update()
        {
            tools.Update();
        }
    }
}