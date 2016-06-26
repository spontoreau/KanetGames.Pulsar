using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pulsar.Toolkit.Security;
using System.Text;

namespace UnitTest.Pulsar.Toolkit
{
    [TestClass]
    public class CipherTest
    {
        [TestMethod]
        public void EncryptSHATest()
        {
            Cipher cipher = new Cipher(CipherType.SHA256);
            string result = cipher.Encrypt("Test");
            string expected = "Uy6qvZV0iA2/drm4zACDLCCm7BE9aCKZVQ16bg80XiU=";

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DecryptSHATest()
        {
            bool actual = false;
            bool expected = true;

            try
            {
                Cipher cipher = new Cipher(CipherType.SHA256);
                cipher.Decrypt("Test");
            }
            catch
            {
                actual = true;
            }

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EncryptDESTest()
        {
            byte[] key = Encoding.ASCII.GetBytes("123456789012345678901234");
            byte[] iv = Encoding.ASCII.GetBytes("12345678");
            Cipher cipher = new Cipher(CipherType.TripleDES, iv, key);
            string result = cipher.Encrypt("Test");
            string expected = "95de80218f527ceb";

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DecryptDESTest()
        {
            byte[] key = Encoding.ASCII.GetBytes("123456789012345678901234");
            byte[] iv = Encoding.ASCII.GetBytes("12345678");
            Cipher cipher = new Cipher(CipherType.TripleDES, iv, key);
            string result = cipher.Decrypt("95de80218f527ceb");
            string expected = "Test";

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void TripleExceptionTest()
        {
            byte[] key = Encoding.ASCII.GetBytes("123456789012345678901234");
            Cipher cipher1 = new Cipher(CipherType.TripleDES, null, key);

            byte[] iv = Encoding.ASCII.GetBytes("12345678");
            Cipher cipher2 = new Cipher(CipherType.TripleDES, iv, null);

            Cipher cipher3 = new Cipher(CipherType.TripleDES);

            bool expected1 = true;
            bool expected2 = true;
            bool expected3 = true;

            bool actual1 = false;
            bool actual2 = false;
            bool actual3 = false;

            try
            {
                cipher1.Encrypt("lol one time");
            }
            catch
            {
                actual1 = true;
            }

            try
            {
                cipher2.Encrypt("lol two time");
            }
            catch
            {
                actual2 = true;
            }

            try
            {
                cipher3.Encrypt("lol three time");
            }
            catch
            {
                actual3 = true;
            }

            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
            Assert.AreEqual(expected3, actual3);

        }
    }
}
