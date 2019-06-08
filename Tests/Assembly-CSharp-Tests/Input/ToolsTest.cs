using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TWF.Input.Tests
{
    [TestClass]
    public class ToolsTest
    {
        private Mock<IToolApplier> toolApplier = new Mock<IToolApplier>();
        private Mock<IMousePositionProvider> mousePositionProvider = new Mock<IMousePositionProvider>(MockBehavior.Strict);
        private Mock<IKeyCombination> toolKeys1 = new Mock<IKeyCombination>();
        private Mock<IKeyCombination> toolKeys2 = new Mock<IKeyCombination>();
        private Mock<IKeyCombination> escape = new Mock<IKeyCombination>();
        private Mock<IKeyCombination> activator = new Mock<IKeyCombination>();
        private Tool tool1 = new Tool("1", "do1", "modify1", "brush1");
        private Tool tool2 = new Tool("2", "do2", "modify2", "brush2");
        private Tools tools;

        public void Setup()
        {
            tools = new Tools(() => toolApplier.Object, mousePositionProvider.Object, escape.Object, activator.Object);
            tools.RegisterTool(toolKeys1.Object, tool1);
            tools.RegisterTool(toolKeys2.Object, tool2);
        }

        [TestMethod]
        public void Update_NoKeyPressed_IsANoOp()
        {
            Setup();

            tools.Update();

            Assert.IsNull(tools.SelectedTool);
            Assert.IsNull(tools.ActiveTool);
        }

        [TestMethod]
        public void Update_SelectTool_SelectsTool()
        {
            toolKeys1.Setup((k) => k.IsActive()).Returns(true);
            Setup();

            tools.Update();

            Assert.AreEqual(tool1, tools.SelectedTool);
            Assert.IsNull(tools.ActiveTool);
        }

        [TestMethod]
        public void Update_ActivateTool_ActivatesTool()
        {
            activator.Setup((a) => a.IsActive()).Returns(true);
            mousePositionProvider.Setup((m) => m.GetMousePosition()).Returns(new Vector(3, 5));
            Setup();

            tools.SelectedTool = tool1;
            tools.Update();

            Assert.AreEqual(tool1, tools.SelectedTool);
            Assert.AreEqual(tool1, tools.ActiveTool);
        }
    }
}