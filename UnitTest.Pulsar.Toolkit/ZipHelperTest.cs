using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pulsar.Toolkit.Helpers;
using System.IO;

namespace UnitTest.Pulsar.Toolkit
{
    [TestClass]
    public class ZipHelperTest
    {
        [TestMethod]
        public void CompressTest()
        {
            byte[] bytes = new byte[1] { 12 };
            byte[] bytesExpected = new byte[21] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 0, 227, 1, 0, 166, 163, 180, 219, 1, 0, 0, 0 };
            MemoryStream ms = new MemoryStream(bytes);
            byte[] compressBytes = ZipHelper.Compress(ms);
            ms.Close();

            Assert.AreEqual(bytesExpected.Length, compressBytes.Length);

            for(int i = 0; i < bytesExpected.Length; i++)
                Assert.AreEqual(bytesExpected[i], compressBytes[i]);
        }

        [TestMethod]
        public void UncompressTest()
        {
            byte[] bytes = new byte[21] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 0, 227, 1, 0, 166, 163, 180, 219, 1, 0, 0, 0 };
            byte[] bytesExpected = new byte[1] { 12 };
            MemoryStream ms = new MemoryStream(bytes);
            byte[] unCompressBytes = ZipHelper.Uncompress(ms);
            ms.Close();

            Assert.AreEqual(bytesExpected.Length, unCompressBytes.Length);

            for (int i = 0; i < bytesExpected.Length; i++)
                Assert.AreEqual(bytesExpected[i], unCompressBytes[i]);
        }
    }
}
