using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TWF.Input.Tests
{
    [TestClass]
    public class ToolsTest
    {
        private Mock<IToolApplier> toolApplier = new Mock<IToolApplier>(MockBehavior.Strict);
        private Mock<IKeyCombination> toolKeys1 = new Mock<IKeyCombination>();
        private Mock<IKeyCombination> toolKeys2 = new Mock<IKeyCombination>();
        private Tool tool1 = new Tool("1", "do1", "modify1", "brush1");
        private Tool tool2 = new Tool("2", "do2", "modify2", "brush2");
        private Tools tools;

        public void Setup()
        {
            tools = new Tools(() => toolApplier.Object);
            tools.RegisterTool(toolKeys1.Object, tool1);
            tools.RegisterTool(toolKeys2.Object, tool2);
        }

        [TestMethod]
        public void EnactTool_NoActiveTool_DoesntDoAnything()
        {
            toolKeys1.Setup((k) => k.IsActive()).Returns(false);
            toolKeys2.Setup((k) => k.IsActive()).Returns(false);

            Setup();

            tools.EnactTool();
        }
    }
}