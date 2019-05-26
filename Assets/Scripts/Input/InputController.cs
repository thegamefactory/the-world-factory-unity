using System;
using System.Collections.Generic;
using UnityEngine;

namespace TWF.Input
{
    public class InputController : MonoBehaviour
    {
        public KeyCode ResidentialModifierKey;
        public KeyCode FarmlandModifierKey;
        public KeyCode RoadModifierKey;

        private Tools tools = new Tools();

        private void Start()
        {
            tools.RegisterTool(
                KeyCombination.builder(ResidentialModifierKey).build(),
                new ToolName("ZoneResidential"),
                pos => Root.GameService.ApplyTool(Zones.Residential.ZonerName(), ToolBrushes.Rectangle.Name, pos));

            tools.RegisterTool(
                KeyCombination.builder(FarmlandModifierKey).build(),
                new ToolName("ZoneFarmland"),
                pos => Root.GameService.ApplyTool(Zones.Farmland.ZonerName(), ToolBrushes.Rectangle.Name, pos));

            tools.RegisterTool(
                KeyCombination.builder(RoadModifierKey).build(),
                new ToolName("BuildRoad"),
                pos => Root.GameService.ApplyTool(Zones.Road.ZonerName(), ToolBrushes.Manatthan.Name, pos));
        }

        void Update()
        {
            tools.Update();
        }
    }
}