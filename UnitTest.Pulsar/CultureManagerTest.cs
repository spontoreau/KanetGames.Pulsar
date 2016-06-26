using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pulsar.Content;
using Pulsar.Core.Configuration;
using Pulsar.Core.Culture;
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
    public class CultureManagerTest
    {
        string path = System.Configuration.ConfigurationManager.AppSettings["Path"];
        string application = System.Configuration.ConfigurationManager.AppSettings["Application"];
        private GraphicsConfiguration _graphics;
        private AudioConfiguration _audio;
        private GameConfiguration _game;
        private HotKeyConfiguration _hotKeys;
        private Language _language;

        [TestInitialize]
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

            this._hotKeys = new HotKeyConfiguration();

            this._language = new Language();
            this._language.Key = CultureInfo.CurrentCulture.Name;
            this._language.Name = CultureInfo.CurrentCulture.NativeName;

            FunctionTranslation func = new FunctionTranslation();
            func.Key = "DEFAULT";

            Translation trans = new Translation();
            trans.Key = "HELLO_WORLD";
            trans.Value = "Bonjour le monde !";

            func.Translations.Add(trans);
            this._language.Functions.Add(func);

            SerializerHelper.Save("Content/graphics.xml", this._graphics);
            SerializerHelper.Save("Content/audio.xml", this._audio);
            SerializerHelper.Save("Content/game.xml", this._game);
            SerializerHelper.Save("Content/keys.xml", this._hotKeys);
            SerializerHelper.Save("Content/" + this._language.Key + ".xml", this._language);
        }

        [TestMethod]
        public void CultureManagerInitializeTest()
        {
            ContentManager content = new ContentManager();
            content.RootDirectory = "Content";

            Dictionary<Type, string> confFiles = new Dictionary<Type, string>();
            confFiles.Add(typeof(GraphicsConfiguration), "graphics.xml");
            confFiles.Add(typeof(AudioConfiguration), "audio.xml");
            confFiles.Add(typeof(GameConfiguration), "game.xml");
            confFiles.Add(typeof(HotKeyConfiguration), "keys.xml");

            ConfigurationManager configurationManager = new ConfigurationManager(confFiles);
            configurationManager.Initialize(content);

            CultureManager cultureManager = new CultureManager();
            cultureManager.Initialize(configurationManager, content);


            Assert.AreEqual(this._language, cultureManager.Current);
            Assert.AreEqual(true, cultureManager.IsInit);
        }
    }
}
