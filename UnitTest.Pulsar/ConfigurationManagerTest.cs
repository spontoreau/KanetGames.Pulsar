using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pulsar.Content;
using Pulsar.Core.Configuration;
using Pulsar.Core.Display;
using Pulsar.Toolkit.Helpers;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace UnitTest.Pulsar
{
    [TestClass]
    public class ConfigurationManagerTest
    {
        string path = System.Configuration.ConfigurationManager.AppSettings["Path"];
        string application = System.Configuration.ConfigurationManager.AppSettings["Application"];
        private GraphicsConfiguration _graphics;
        private AudioConfiguration _audio;
        private GameConfiguration _game;
        private HotKeyConfiguration _hotKeys;

        [TestInitialize()]
        public void Init()
        {
            if (Directory.Exists(path + "/" + application))
            {
                string[] files = Directory.GetFiles(path + "/" + application);
                if (files.Length > 0)
                {
                    foreach (string file in files)
                        File.Delete(file);
                }
            }

            if (!Directory.Exists("Content"))
                Directory.CreateDirectory("Content");

            this._graphics = new GraphicsConfiguration();
            this._graphics.BitsPerPixel = 32;
            this._graphics.IsMouseVisible = true;
            this._graphics.IsVerticalSync = false;
            this._graphics.Resolution = new Resolution(800, 600);
            this._graphics.Styles = Styles.Default;
            this._graphics.IsLightEngineActive = false;
            this._graphics.IsParticuleEngineActive = false;

            this._audio = new AudioConfiguration();
            this._audio.FadeMusic = false;
            this._audio.FxVolume = 0f;
            this._audio.MasterVolume = 0f;
            this._audio.MaxSounds = 0;
            this._audio.MusicVolume = 0f;
            this._audio.Mute = true;

            this._game = new GameConfiguration();
            this._game.Culture = CultureInfo.CurrentCulture.Name;
            this._game.Skin = string.Empty;
            this._game.SkinFont = string.Empty;

            this._hotKeys = new HotKeyConfiguration();

            SerializerHelper.Save("Content/graphics.xml", this._graphics);
            SerializerHelper.Save("Content/audio.xml", this._audio);
            SerializerHelper.Save("Content/game.xml", this._game);
            SerializerHelper.Save("Content/keys.xml", this._hotKeys);
        }

        [TestMethod]
        public void ConfigurationManagerInitializeTest()
        {
            ContentManager content = new ContentManager();
            content.RootDirectory = "Content";

            Dictionary<Type, string> confFiles = new Dictionary<Type, string>();
            confFiles.Add(typeof(GraphicsConfiguration), "graphics.xml");
            confFiles.Add(typeof(AudioConfiguration), "audio.xml");
            confFiles.Add(typeof(GameConfiguration), "game.xml");
            confFiles.Add(typeof(HotKeyConfiguration), "keys.xml");

            ConfigurationManager manager = new ConfigurationManager(confFiles);
            manager.Initialize(content);

            Assert.AreEqual(this._graphics, manager.Graphics);
            Assert.AreEqual(this._audio, manager.Audio);
            Assert.AreEqual(this._game, manager.Game);
            Assert.AreEqual(this._hotKeys, manager.HotKey);
            Assert.AreEqual(true, manager.IsInit);
        }
    }
}
