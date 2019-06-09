using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace TWF.Input.Tests
{
    [TestClass]
    public class ToolsTest
    {
        private readonly Mock<IToolApplier> toolApplier = new Mock<IToolApplier>(MockBehavior.Strict);
        private readonly Mock<IMousePositionProvider> mousePositionProvider = new Mock<IMousePositionProvider>(MockBehavior.Strict);
        private readonly FakeKeyCombination toolKeys1 = new FakeKeyCombination();
        private readonly FakeKeyCombination toolKeys2 = new FakeKeyCombination();
        private readonly FakeKeyCombination escape = new FakeKeyCombination();
        private readonly FakeKeyCombination activator = new FakeKeyCombination();
        private readonly Tool tool1 = new Tool("1", "do1", "modify1", "brush1", null);
        private readonly Tool tool2 = new Tool("2", "do2", "modify2", "brush2", null);
        private Tools tools;

        public void Initialize()
        {
            IToolApplier ta = toolApplier.Object;
            tools = new Tools(() => ta, mousePositionProvider.Object, escape, activator);
            tools.RegisterTool(toolKeys1, tool1);
            tools.RegisterTool(toolKeys2, tool2);
        }

        [TestMethod]
        public void Update_NoKeyPressed_IsANoOp()
        {
            Initialize();

            tools.Update();

            Assert.IsNull(tools.SelectedTool);
            Assert.IsNull(tools.ActiveTool);
        }



        [TestMethod]
        public void Update_SelectTool_SelectsTool()
        {
            Initialize();

            toolKeys1.Active = true;
            tools.Update();

            Assert.AreEqual(tool1, tools.SelectedTool);
            Assert.IsNull(tools.ActiveTool);
        }

        [TestMethod]
        public void Update_StopsUsingSelectTool_SelectsToolRemains()
        {
            Initialize();

            toolKeys1.Active = true;
            tools.Update();
            toolKeys1.Active = false;
            tools.Update();

            Assert.AreEqual(tool1, tools.SelectedTool);
            Assert.IsNull(tools.ActiveTool);
        }

        [TestMethod]
        public void Update_ActivateTool_ActivatesTool()
        {
            mousePositionProvider.Setup((m) => m.GetMousePosition()).Returns(new Vector(3, 5));
            var previewOutcome = SetupApplierCallWithTool1(new Vector(3, 5));

            Initialize();

            activator.Active = true;
            tools.SelectedTool = tool1;
            tools.Update();

            Assert.AreEqual(tool1, tools.ActiveTool);
            Assert.AreEqual(previewOutcome, tools.CurrentPreviewOutcome);
        }

        [TestMethod]
        public void Update_SwitchesToolWhileActive_DeactivatesToolAndSwitches()
        {
            Initialize();

            activator.Active = true;
            tools.ActiveTool = tool1;
            toolKeys2.Active = true;
            tools.Update();

            Assert.AreEqual(tool2, tools.SelectedTool);
            Assert.AreEqual(null, tools.ActiveTool);
            Assert.AreEqual(null, tools.CurrentPreviewOutcome);
        }

        [TestMethod]
        public void Update_ActiveOnMultiplePositions_CallsPreviewForEachPosition()
        {
            activator.Active = true;
            mousePositionProvider.SetupSequence((m) => m.GetMousePosition()).Returns(new Vector(3, 5)).Returns(new Vector(2, 5)).Returns(new Vector(2, 6));
            var previewOutcome1 = SetupApplierCallWithTool1(new Vector(3, 5));
            var previewOutcome2 = SetupApplierCallWithTool1(new Vector(3, 5), new Vector(2, 5));
            var previewOutcome3 = SetupApplierCallWithTool1(new Vector(3, 5), new Vector(2, 5), new Vector(2, 6));

            Initialize();

            tools.SelectedTool = tool1;
            tools.Update();
            Assert.AreEqual(previewOutcome1, tools.CurrentPreviewOutcome);
            tools.Update();
            Assert.AreEqual(previewOutcome2, tools.CurrentPreviewOutcome);
            tools.Update();
            Assert.AreEqual(previewOutcome3, tools.CurrentPreviewOutcome);
        }

        [TestMethod]
        public void Update_CommitTool_CallsPreviewForEachPositionAndApplyAtTheEnd()
        {
            activator.Active = true;
            mousePositionProvider.SetupSequence((m) => m.GetMousePosition())
                .Returns(new Vector(3, 5))
                .Returns(new Vector(2, 5))
                .Returns(new Vector(2, 6))
                .Returns(new Vector(2, 6));
            SetupApplierCallsWithTool1(new Vector(3, 5), new Vector(2, 5), new Vector(2, 6));
            toolApplier
                .Setup(ta => ta.ApplyTool(
                    "do1",
                    "modify1",
                    "brush1",
                    It.Is<IEnumerable<Vector>>(p => new List<Vector> { new Vector(3, 5), new Vector(2, 5), new Vector(2, 6) }.SequenceEqual(p))))
                .Returns(ToolOutcome.SUCCESS);

            Initialize();

            tools.SelectedTool = tool1;
            tools.Update();
            tools.Update();
            tools.Update();
            activator.Active = false;
            tools.Update();

            toolApplier.VerifyAll();
            Assert.AreEqual(null, tools.ActiveTool);
            Assert.AreEqual(tool1, tools.SelectedTool);
        }

        private void SetupApplierCallsWithTool1(params Vector[] positions)
        {
            List<Vector> positionList = new List<Vector>(positions);
            for (int count = 1; count <= positionList.Count; ++count)
            {
                SetupApplierCallWithTool1(positionList.GetRange(0, count));
            }
        }

        private ToolPreviewOutcome SetupApplierCallWithTool1(params Vector[] positions)
        {
            return SetupApplierCallWithTool1(new List<Vector>(positions));
        }

        private ToolPreviewOutcome SetupApplierCallWithTool1(List<Vector> positions)
        {
            var previewOutcome = PreviewOutcomeFor(positions);
            toolApplier
                .Setup(ta => ta.AddPosition(
                    It.IsAny<string>(),
                    It.IsAny<LinkedList<Vector>>(),
                    It.IsAny<Vector>()))
                .Callback<string, LinkedList<Vector>, Vector>((_, l, v) => l.AddLast(v));
            toolApplier
                .Setup(ta => ta.PreviewTool(
                    "do1",
                    "modify1",
                    "brush1",
                    It.Is<IEnumerable<Vector>>(p => positions.SequenceEqual(p))))
                .Returns(previewOutcome);
            return previewOutcome;
        }

        static private ToolPreviewOutcome PreviewOutcomeFor(List<Vector> positions)
        {
            var previewOutcomeBuilder = ToolPreviewOutcome.Builder();
            foreach (Vector p in positions)
            {
                previewOutcomeBuilder.WithPositionOutcome(p, ToolOutcome.SUCCESS);
            }
            return previewOutcomeBuilder.Build();
        }

        class FakeKeyCombination : IKeyCombination
        {
            public bool Active { get; set; }
            public bool IsActive()
            {
                return Active;
            }
        }
    }
}