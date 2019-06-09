using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

namespace TWF.Graphics.Test
{
    [TestClass]
    public class ColorUtilsTest
    {
        Color result;

        [TestMethod]
        public void Superpose_FrontTransparent_UsesBack()
        {
            Color front = new Color(0.1f, 0.2f, 0.3f, 0.0f);
            Color back = new Color(0.5f, 0.6f, 0.7f, 0.2f);
            ColorUtils.Superpose(ref result, front, back);

            Assert.AreEqual(back, result);
        }

        [TestMethod]
        public void Superpose_FrontOpaque_UsesFront()
        {
            Color front = new Color(0.1f, 0.2f, 0.3f, 1.0f);
            Color back = new Color(0.5f, 0.6f, 0.7f, 0.2f);
            ColorUtils.Superpose(ref result, front, back);

            Assert.AreEqual(front, result);
        }

        [TestMethod]
        public void Superpose_FrontSemiTransparent_Interpolates()
        {
            Color front = new Color(0.1f, 0.2f, 0.3f, 0.1f);
            Color back = new Color(0.3f, 0.4f, 0.5f, 0.9f);
            ColorUtils.Superpose(ref result, front, back);

            Assert.AreEqual(0.28f, result.r, 0.001f);
            Assert.AreEqual(0.38f, result.g, 0.001f);
            Assert.AreEqual(0.48f, result.b, 0.001f);
            Assert.AreEqual(0.91f, result.a, 0.001f);
        }
    }
}
