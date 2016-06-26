using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Pulsar.Toolkit.Diagnostics;
using Pulsar.Toolkit.Helpers;

namespace UnitTest.Pulsar.Toolkit
{
    [TestClass]
    public class SerializeHelperTest
    {
        [TestInitialize]
        public void Clear()
        {
            if(!Directory.Exists("Serialize"))
            {
                Directory.CreateDirectory("Serialize");
            }
            else
            {
                string [] files = Directory.GetFiles("Serialize");
                foreach(string file in files)
                    File.Delete(file);
            }
        }

        [TestMethod]
        public void SaveBinary()
        {
            Trace trace = new Trace("Trace", "Trace", TraceState.Information);
            SerializerHelper.Save("Serialize/test.bin", trace);

            Assert.AreEqual(true, File.Exists("Serialize/test.bin"));
        }

        [TestMethod]
        public void SaveXml()
        {
            Trace trace = new Trace("Trace", "Trace", TraceState.Information);
            SerializerHelper.Save("Serialize/test.xml", trace);

            Assert.AreEqual(true, File.Exists("Serialize/test.xml"));
        }

        [TestMethod]
        public void LoadBinary()
        {
            Trace expected = new Trace("Trace", "Trace", TraceState.Information);
            SerializerHelper.Save("Serialize/test.bin", expected);
            Trace actual = SerializerHelper.Load<Trace>("Serialize/test.bin");

            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.Source, actual.Source);
            Assert.AreEqual(expected.State, actual.State);
        }

        [TestMethod]
        public void LoadXml()
        {
            Trace expected = new Trace("Trace", "Trace", TraceState.Information);
            SerializerHelper.Save("Serialize/test.xml", expected);
            Trace actual = SerializerHelper.Load<Trace>("Serialize/test.xml");

            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.Source, actual.Source);
            Assert.AreEqual(expected.State, actual.State);
        }
    }
}
