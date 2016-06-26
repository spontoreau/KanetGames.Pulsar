using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pulsar.Framework;

namespace UnitTest.Pulsar.Framework
{
    [TestClass]
    public class MathTest
    {
        [TestMethod]
        public void PiTest()
        {
            float actual = MathHelper.Pi;
            float expected = (float)Math.PI;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TwoPiTest()
        {
            float actual = MathHelper.TwoPi;
            float expected = (float)Math.PI * 2;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ETest()
        {
            float actual = MathHelper.E;
            float expected = (float)Math.E;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AbsTest()
        {
            float actual = MathHelper.Abs(-1f);
            float expected = 1f;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SqrtTest()
        {
            float actual = MathHelper.Sqrt(9f);
            float expected = 3f;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SquareTest()
        {
            float actual = MathHelper.Square(3f);
            float expected = 9f;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PowTest()
        {
            float actual = MathHelper.Pow(3f, 2f);
            float expected = 9f;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ACosTest()
        {
            float actual = MathHelper.Round(MathHelper.Acos(0f), 6);
            float expected = 1.570796f;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ASinTest()
        {
            float actual = MathHelper.Round(MathHelper.Asin(1f), 6);
            float expected = 1.570796f;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ATanTest()
        {
            float actual = MathHelper.Round(MathHelper.Atan(1f), 6);
            float expected = 0.785398f;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ATan2Test()
        {
            float actual = MathHelper.Round(MathHelper.Atan2(1f, 1f), 6);
            float expected = 0.785398f;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CosTest()
        {
            float actual = MathHelper.Round(MathHelper.Cos(MathHelper.Pi / 2f), 6);
            float expected = 0f;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SinTest()
        {
            float actual = MathHelper.Round(MathHelper.Sin(MathHelper.Pi / 2f), 6);
            float expected = 1f;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TanTest()
        {
            float actual = MathHelper.Round(MathHelper.Tan(MathHelper.Pi / 4f), 6);
            float expected = 1f;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExpTest()
        {
            float actual = MathHelper.Round(MathHelper.Exp(1.0f), 6);
            float expected = MathHelper.Round(MathHelper.E, 6);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LogTest()
        {
            float actual = MathHelper.Round(MathHelper.Log(MathHelper.E), 6);
            float expected = 1f;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Log10Test()
        {
            float actual = MathHelper.Log10(10f);
            float expected = 1f;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Lerp()
        {
            float actual = MathHelper.Lerp(1, 2, 0.5f);
            float expected = 1.5f;

            Assert.AreEqual(expected, actual);
        }
    }
}
