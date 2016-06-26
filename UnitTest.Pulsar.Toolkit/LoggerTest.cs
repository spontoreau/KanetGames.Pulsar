using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pulsar.Toolkit;
using System.Configuration;
using System.IO;
using System.Text;
using Pulsar.Toolkit.Diagnostics;

namespace UnitTest.Pulsar.Toolkit
{
    [TestClass]
    public class LoggerTest
    {
        string path = ConfigurationManager.AppSettings["Path"];
        string application = ConfigurationManager.AppSettings["Application"];

        [TestInitialize()]
        public void ClearFile()
        {
            if(Directory.Exists(path + "/" + application))
            {
                string[] files = Directory.GetFiles(path + "/" + application);
                if (files.Length > 0)
                {
                    foreach (string file in files)
                        File.Delete(file);
                }
            }
        }

        [TestMethod]
        public void LoggerInitializeTest()
        {
            Logger.Initialize(path, application);

            Assert.AreEqual(true, Logger.IsInitialize());
            Assert.AreEqual(true, Directory.Exists(path + "/" + application));
        }

        [TestMethod]
        public void TraceStringTest()
        {
            Logger.Initialize(path, application);
            Logger.Trace("TestUnit", "This is a message", TraceState.Information);

            string[] files = Directory.GetFiles(path + "/" + application);

            Assert.AreEqual(1, files.Length);

            string content = File.ReadAllText(files[0], Encoding.UTF8);

            Assert.AreEqual(true, content.Contains("TestUnit"));            
            Assert.AreEqual(true, content.Contains("This is a message"));            
            Assert.AreEqual(true, content.Contains(TraceState.Information.ToString()));
        }

        [TestMethod]
        public void TraceExceptionTest()
        {
            Logger.Initialize(path, application);
            Exception ex = new Exception("This is a message");
            ex.Source = typeof(LoggerTest).FullName;
            Logger.Trace(ex);

            string[] files = Directory.GetFiles(path + "/" + application);

            Assert.AreEqual(1, files.Length);

            string content = File.ReadAllText(files[0], Encoding.UTF8);

            Assert.AreEqual(true, content.Contains(ex.Source));            
            Assert.AreEqual(true, content.Contains(ex.Message));            
            Assert.AreEqual(true, content.Contains(TraceState.Error.ToString()));
        }

        [TestMethod]
        public void TracePulsarExceptionTest()
        {
            Logger.Initialize(path, application);
            PulsarException ex = new PulsarException(typeof(LoggerTest).FullName, "This is a message");

            string[] files = Directory.GetFiles(path + "/" + application);

            Assert.AreEqual(1, files.Length);

            string content = File.ReadAllText(files[0], Encoding.UTF8);

            Assert.AreEqual(true, content.Contains(ex.Source));
            Assert.AreEqual(true, content.Contains(ex.Message));
            Assert.AreEqual(true, content.Contains(TraceState.Error.ToString()));
        }

        [TestMethod]
        public void TracePulsarExceptionWithInnerTest()
        {
            Logger.Initialize(path, application);
            Exception innerEx = new Exception("This is the inner");
            innerEx.Source = typeof(LoggerTest).FullName;
            PulsarException pex = new PulsarException(typeof(LoggerTest).FullName, "This is a message", innerEx);

            string[] files = Directory.GetFiles(path + "/" + application);

            Assert.AreEqual(1, files.Length);

            string content = File.ReadAllText(files[0], Encoding.UTF8);

            Assert.AreEqual(true, content.Contains(innerEx.Source));
            Assert.AreEqual(true, content.Contains(innerEx.Message));
            Assert.AreEqual(true, content.Contains(TraceState.Error.ToString()));

            Assert.AreEqual(true, content.Contains(pex.Source));
            Assert.AreEqual(true, content.Contains(pex.Message));
        }
    }
}
