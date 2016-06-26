using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pulsar.Core.Display;
using Pulsar.Framework;

namespace UnitTest.Pulsar
{
    [TestClass]
    public class DiplayTest
    {
        [TestMethod]
        public void ResolutionCtorTest()
        {
            Rectangle boundsExpected = new Rectangle(0f, 0f, 800f, 600f);
            Resolution actual = new Resolution(800, 600);
            Assert.AreEqual(boundsExpected, actual.Bounds);
        }

        [TestMethod]
        public void ResolutionParseTest()
        {
            Resolution expected = new Resolution(1280, 720);
            Resolution actual = Resolution.Parse(VirtualResolutionEnum.WXGA);
            Assert.AreEqual(expected.Bounds, actual.Bounds);
        }
    }
}
