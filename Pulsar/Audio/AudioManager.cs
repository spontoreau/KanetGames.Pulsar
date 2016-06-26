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
using Pulsar.Core.Configuration;
using Pulsar.Framework;
using SFML.Audio;
using System;

namespace Pulsar.Audio
{
    /// <summary>
    /// Manage audio
    /// </summary>
    public class AudioManager : GameComponent
    {
        /// <summary>
        /// Event raise when music stopped
        /// </summary>
        public event EventHandler MusicStopped;

        private ContentManager _contentManager;

        /// <summary>
        /// Current playing musics
        /// </summary>
        private Music _currentMusic = null;

        /// <summary>
        /// Current playing list of fx sound
        /// </summary>
        private Sound[] _playingSounds;

        /// <summary>
        /// True if music is in pause
        /// </summary>
        private bool _isMusicPaused = false;

        /// <summary>
        /// Current playing song name
        /// </summary>
        public string CurrentMusicName { get; private set; }

        private float _realMusicVolume;

        private float _realFxVolume;

        /// <summary>
        /// Creates a new Audio Manager. Add this to the Components collection of your Game.
        /// </summary>
        /// <param name="game">The Game</param>
        public AudioManager(Game game)
            : base(game)
        {
        
        }

        /// <summary>
        /// Initialize the AudioManager
        /// </summary>
        public void Initialize(ContentManager content, ConfigurationManager configurationManager)
        {
            this._contentManager = content;
            this._playingSounds = new Sound[configurationManager.Audio.MaxSounds];
            this._realFxVolume = configurationManager.Audio.MasterVolume * configurationManager.Audio.FxVolume;
            this._realMusicVolume = configurationManager.Audio.MasterVolume * configurationManager.Audio.MusicVolume;

            configurationManager.AudioConfigurationChanged += AudioConfigurationChanged;

            base.Initialize();
        }

        /// <summary>
        /// Handle the AudioConfiguration Changed
        /// </summary>
        /// <param name="sender">AudioManager</param>
        /// <param name="e">Event arguments</param>
        private void AudioConfigurationChanged(object sender, EventArgs e)
        {
            ConfigurationManager configurationManager = (ConfigurationManager)sender;
            this._realFxVolume = configurationManager.Audio.MasterVolume * configurationManager.Audio.FxVolume;
            this._realMusicVolume = configurationManager.Audio.MasterVolume * configurationManager.Audio.MusicVolume;

            if(this._currentMusic != null && this._currentMusic.Status != SoundStatus.Stopped)
                this._currentMusic.Volume = (configurationManager.Audio.Mute) ? 0f : this._realMusicVolume;

            foreach (Sound sound in this._playingSounds)
            {
                if (sound != null & sound.Status != SoundStatus.Stopped)
                {
                    sound.Volume = (configurationManager.Audio.Mute) ? 0f : this._realFxVolume;
                }
            }
        }

        /// <summary>
        /// Starts playing the song with the given name. If it is already playing, this method
        /// does nothing. If another song is currently playing, it is stopped first.
        /// </summary>
        /// <param name="songName">Name of the song to play</param>
        /// <param name="loop">True if song should loop, false otherwise</param>
        public void PlayMusic(string filePath, bool loop)
        {
            if (CurrentMusicName != filePath)
            {
                if (_currentMusic != null)
                {
                    this._currentMusic.Stop();
                    this._currentMusic.Dispose();
                }

                Music music = new Music(filePath);
                music.Loop = loop;

                this._currentMusic = music;

                CurrentMusicName = filePath;

                _isMusicPaused = false;
                music.Play();
            }
        }

        /// <summary>
        /// Pauses the currently playing song. This is a no-op if the song is already paused,
        /// or if no song is currently playing.
        /// </summary>
        public void PauseMusic()
        {
            if (Enabled)
            {
                if (_currentMusic != null && !_isMusicPaused)
                {
                    _currentMusic.Pause();
                    _isMusicPaused = true;
                }
            }
        }

        /// <summary>
        /// Resumes the currently paused song. This is a no-op if the song is not paused,
        /// or if no song is currently playing.
        /// </summary>
        public void ResumeMusic()
        {
            if (Enabled)
            {
                if (_currentMusic != null && _isMusicPaused)
                {
                    if (Enabled) _currentMusic.Play();
                    _isMusicPaused = false;
                }
            }
        }

        /// <summary>
        /// Stops the currently playing song. This is a no-op if no song is currently playing.
        /// </summary>
        public void StopMusic()
        {
            if (_currentMusic != null && _currentMusic.Status != SoundStatus.Stopped)
            {
                _currentMusic.Stop();
                _isMusicPaused = false;
            }
        }

        /// <summary>
        /// Plays the sound of the given name.
        /// </summary>
        /// <param name="soundName">Name of the sound</param>
        public void PlaySound(string soundKey)
        {
            if (Enabled)
            {
                int index = GetAvailableIndex();

                if (index != -1)
                {
                    SoundBuffer buffer = this._contentManager.Load<SoundBuffer>(soundKey);
                    Sound sound = new Sound(buffer);

                    if (_playingSounds[index] != null)
                        this._playingSounds[index].Dispose();

                    _playingSounds[index] = sound;
                    _playingSounds[index].Volume = this._realFxVolume;
                    _playingSounds[index].Play();
                }
            }
        }

        /// <summary>
        /// Stops all currently playing sounds.
        /// </summary>
        private void StopAllSounds()
        {
            for (int i = 0; i < _playingSounds.Length; ++i)
            {
                if (_playingSounds[i] != null)
                {
                    _playingSounds[i].Stop();
                    _playingSounds[i].Dispose();
                    _playingSounds[i] = null;
                }
            }
        }

        /// <summary>
        /// Called per loop unless Enabled is set to false.
        /// </summary>
        /// <param name="gameTime">Time elapsed since last frame</param>
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < _playingSounds.Length; ++i)
            {
                if (_playingSounds[i] != null && _playingSounds[i].Status == SoundStatus.Stopped)
                {
                    _playingSounds[i].Dispose();
                    _playingSounds[i] = null;
                }
            }

            if (_currentMusic != null && _currentMusic.Status == SoundStatus.Stopped)
            {
                _currentMusic.Dispose();
                _currentMusic = null;
                CurrentMusicName = null;
                _isMusicPaused = false;
                OnMusicStopped(EventArgs.Empty);
            }           

            base.Update(gameTime);
        }

        /// <summary>
        /// Raise the on music stopped
        /// </summary>
        /// <param name="args">Event arguments</param>
        private void OnMusicStopped(EventArgs args)
        {
            EventHandler tmp = MusicStopped;

            if (tmp != null)
            {
                tmp(this, args);
            }
        }

        /// <summary>
        /// Called when the Enabled property changes. Raises the EnabledChanged event.
        /// </summary>
        /// <param name="args">Arguments to the EnabledChanged event.</param>
        protected override void OnEnabledChanged(EventArgs args)
        {
            if (!Enabled)
            {
                StopAllSounds();
                StopMusic();
            }

            base.OnEnabledChanged(args);
        }

        /// <summary>
        /// Return a avaible index in the PlayingSound collection
        /// </summary>
        /// <returns></returns>
        private int GetAvailableIndex()
        {
            for (int i = 0; i < _playingSounds.Length; ++i)
            {
                if (_playingSounds[i] == null)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
