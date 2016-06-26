/* License
 * 
 * The MIT License (MIT)
 *
 * Copyright (c) 2013, Kanet Games (contact@kanetgames.com / www.kanetgames.com)
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using Pulsar.Content;
using System;
using System.Collections.Generic;

namespace Pulsar.Core.Configuration
{
    /// <summary>
    /// Manage engine configuration files.
    /// </summary>
    public sealed class ConfigurationManager
    {
        private ContentManager _content;

        /// <summary>
        /// Graphics configruation object
        /// </summary>
        private GraphicsConfiguration _graphics;

        /// <summary>
        /// Audio configuration object
        /// </summary>
        private AudioConfiguration _audio;

        /// <summary>
        /// Game configuration object
        /// </summary>
        private GameConfiguration _game;

        /// <summary>
        /// HotKey configuration object
        /// </summary>
        private HotKeyConfiguration _hotKey;

        /// <summary>
        /// Graphics configruation object
        /// </summary>
        public GraphicsConfiguration Graphics 
        {
            get
            {
                return this._graphics;
            }
            set
            {
                if (this._graphics == null || !this._graphics.Equals(value))
                {
                    this._graphics = value;
                    OnGraphicsConfigurationChanged();
                }
            }
        }

        /// <summary>
        /// Audio configuration object
        /// </summary>
        public AudioConfiguration Audio 
        {
            get
            {
                return this._audio;
            }
            set
            {
                if (this._audio == null || !this._audio.Equals(value))
                {
                    this._audio = value;
                    OnAudioConfigurationChanged();
                }
            }
        }

        /// <summary>
        /// Game configuration object
        /// </summary>
        public GameConfiguration Game 
        {
            get
            {
                return this._game;
            }
            set
            {
                if (this._game == null || !this._game.Equals(value))
                {
                    this._game = value;
                    OnGameConfigurationChanged();
                }
            }
        }

        /// <summary>
        /// HotKey configuration object
        /// </summary>
        public HotKeyConfiguration HotKey 
        {
            get
            {
                return this._hotKey;
            }
            set
            {
                if (this._hotKey == null || !this._hotKey.Equals(value))
                {
                    this._hotKey = value;
                    OnHotKeyConfigurationChanged();
                }
            }
        }

        /// <summary>
        /// Raise when GraphicsConfiguration changed
        /// </summary>
        public event EventHandler GraphicsConfigurationChanged;

        /// <summary>
        /// Raise when AudioConfiguration changed
        /// </summary>
        public event EventHandler AudioConfigurationChanged;

        /// <summary>
        /// Raise when GameConfiguration changed
        /// </summary>
        public event EventHandler GameConfigurationChanged;

        /// <summary>
        /// Raise when HatKeyConfiguration changed
        /// </summary>
        public event EventHandler HotKeyConfigurationChanged;

        /// <summary>
        /// True if Configuration manager is initialize
        /// </summary>
        public bool IsInit { get; private set; }

        /// <summary>
        /// Dictionary conresponding to a pair of configuration type / configuration file name
        /// </summary>
        private Dictionary<Type, string> ConfigurationFileNames { get; set; }

        /// <summary>
        /// Create a new instance of ConfigurationManager
        /// </summary>
        /// <param name="rootDirectory">The root configuration directory</param>
        public ConfigurationManager(Dictionary<Type, string> configurationFileNames)
        {
            ConfigurationFileNames = configurationFileNames;
        }

        /// <summary>
        /// Initialize the configuration manager
        /// </summary>
        public void Initialize(ContentManager content)
        {
            if (!this.IsInit)
            {
                this._content = content;

                try
                {
                    InitGraphics();
                    InitAudio();
                    InitGame();
                    InitHotKey();

                    IsInit = true;
                }
                catch (Exception ex)
                {
                    throw new CoreEngineException(typeof(ConfigurationManager).FullName, string.Format(Pulsar.Resources.Exceptions.Common.InitializeFailed, typeof(ConfigurationManager).FullName), ex);
                }
            }
            else
            {
                throw new CoreEngineException(typeof(ConfigurationManager).FullName, string.Format(Pulsar.Resources.Exceptions.Common.InitializeHasDone, typeof(ConfigurationManager).FullName));
            }
        }

        /// <summary>
        /// Get the complete configuration file path
        /// </summary>
        /// <param name="type">Type of the key / value pair</param>
        /// <returns>File name</returns>
        private string GetConfigurationFilePath(Type type)
        {
            string config = string.Empty;
            bool isKeyDefine = this.ConfigurationFileNames.TryGetValue(type, out config);

            if (!isKeyDefine)
                throw new CoreEngineException(typeof(ConfigurationManager).FullName, string.Format(Pulsar.Resources.Exceptions.ConfigurationManager.KeyNotDefine, type.FullName));
            else
            {
                if(string.IsNullOrEmpty(config))
                    throw new CoreEngineException(typeof(ConfigurationManager).FullName, string.Format(Pulsar.Resources.Exceptions.ConfigurationManager.FileNameNotDefine, type.FullName));
            }

            return config;
        }

        /// <summary>
        /// Initialize graphics configuration
        /// </summary>
        private void InitGraphics()
        {
            string config = GetConfigurationFilePath(typeof(GraphicsConfiguration));
            GraphicsConfiguration graphicsConfiguration = this._content.Load<GraphicsConfiguration>(config, false);
            this.Graphics = graphicsConfiguration;
        }

        /// <summary>
        /// Initialize audio configuration
        /// </summary>
        private void InitAudio()
        {
            string config = GetConfigurationFilePath(typeof(AudioConfiguration));
            AudioConfiguration audioConfiguration = this._content.Load<AudioConfiguration>(config, false);
            this.Audio = audioConfiguration;
        }

        /// <summary>
        /// Initialize game configuration
        /// </summary>
        private void InitGame()
        {
            string config = GetConfigurationFilePath(typeof(GameConfiguration));
            GameConfiguration gameConfiguration = this._content.Load<GameConfiguration>(config, false);
            this.Game = gameConfiguration;
        }

        /// <summary>
        /// Initialize hot key configuration
        /// </summary>
        private void InitHotKey()
        {
            string config = GetConfigurationFilePath(typeof(HotKeyConfiguration));
            HotKeyConfiguration hotKeyConfiguration = this._content.Load<HotKeyConfiguration>(config, false);
            this.HotKey = hotKeyConfiguration;
        }

        /// <summary>
        /// Raise the graphics configuration changed
        /// </summary>
        private void OnGraphicsConfigurationChanged()
        {
            EventHandler tmp = GraphicsConfigurationChanged;

            if (tmp != null)
                GraphicsConfigurationChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raise the audio configuration changed
        /// </summary>
        private void OnAudioConfigurationChanged()
        {
            EventHandler tmp = AudioConfigurationChanged;

            if (tmp != null)
                AudioConfigurationChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raise the game configuration changed
        /// </summary>
        private void OnGameConfigurationChanged()
        {
            EventHandler tmp = GameConfigurationChanged;

            if (tmp != null)
                GameConfigurationChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raise the hotkey configuration changed
        /// </summary>
        private void OnHotKeyConfigurationChanged()
        {
            EventHandler tmp = HotKeyConfigurationChanged;

            if (tmp != null)
                HotKeyConfigurationChanged(this, EventArgs.Empty);
        }
    }
}
