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

using System;
using System.ComponentModel;

namespace Pulsar.Core.Configuration
{
    /// <summary>
    /// Audio configuration object. This class define the audio configuration to manage in the audio manager. <br/>
    /// </summary>
    [Serializable()]
    public sealed class AudioConfiguration
    {
        private float _fxVolume;        
        private float _musicVolume;
        private float _masterVolume;      

        /// <summary>
        /// Fx Volume of the audio configuration. <br/>
        /// Ranging from 0.0f (silence) to 1.0f (full volume). 
        /// Full volume is relative to the master volume.<br/>
        /// </summary>
        [Category("Volume"), Description("Effect volume (value between 0.0 and 1.0).")]
        public float FxVolume
        {
            get 
            { 
                return this._fxVolume; 
            }
            set
            {
                if (value < 0.0f)
                    this._fxVolume = 0.0f;
                else if (value > 1.0f)
                    this._fxVolume = 1.0f;
                else
                    this._fxVolume = value;
            }
        }

        /// <summary>
        /// Music Volume of the audio configuration. <br/>
        /// Ranging from 0.0f (silence) to 1.0f (full volume). <br/>
        /// Full volume is relative to the master volume.<br/>
        /// </summary>
        [Category("Volume"), Description("Music volume (value between 0.0 and 1.0).")]
        public float MusicVolume
        {
            get 
            { 
                return this._musicVolume; 
            }
            set 
            {
                if (value < 0.0f)
                    this._musicVolume = 0.0f;
                else if (value > 1.0f)
                    this._musicVolume = 1.0f;
                else
                    this._musicVolume = value;
            }
        }

        /// <summary>
        /// Master Volume of the audio configuration.<br/>
        /// Ranging from 0.0f (silence) to 1.0f (full volume).
        /// </summary>
        [Category("Volume"), Description("Master volume (value between 0.0 and 1.0).")]
        public float MasterVolume
        {
            get 
            { 
                return this._masterVolume; 
            }
            set 
            {
                if (value < 0.0f)
                    this._masterVolume = 0.0f;
                else if (value > 1.0f)
                    this._masterVolume = 1.0f;
                else
                    this._masterVolume = value;
            }
        }

        /// <summary>
        /// True if the audio engine fade a Music when changing for an other one.
        /// </summary>
        [Category("Parameters"), Description("Fade music when change the cue in the playlist.")]
        public bool FadeMusic { get; set;}

        /// <summary>
        /// Max sound the audio engine can play un paralele
        /// </summary>
        [Category("Parameters"), Description("Max sounds which can be play in the engine at the same time.")]
        public int MaxSounds { get; set; }

        /// <summary>
        /// True if the audio engine is cut
        /// </summary>
        [Category("Parameters"), Description("Mute the audio engine.")]
        public bool Mute { get; set; }

        /// <summary>
        /// Construct a new AudioConfiguration object
        /// </summary>
        public AudioConfiguration()
            :base()
        {
            
        }

        /// <summary>
        /// Define if an object is equals with the current object instance
        /// </summary>
        /// <param name="obj">Obj to compare</param>
        /// <returns>True if equals</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            return (obj is AudioConfiguration) ? this.Equals((AudioConfiguration)obj) : false;
        }

        /// <summary>
        /// Define if an AudioConfiguration object is equals with the current object instance
        /// </summary>
        /// <param name="conf">AudioConfiguration to compare</param>
        /// <returns>True if equals</returns>
        public bool Equals(AudioConfiguration conf)
        {
            if (conf == null)
                return false;

            return (this.Mute == conf.Mute
                && this.FadeMusic == conf.FadeMusic
                && this.FxVolume == conf.FxVolume
                && this.MasterVolume == conf.MasterVolume
                && this.MaxSounds == conf.MaxSounds
                && this.MusicVolume == conf.MusicVolume);
        }

        /// <summary>
        /// Get the HashCode of the object
        /// </summary>
        /// <returns>HashCode of the object</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
