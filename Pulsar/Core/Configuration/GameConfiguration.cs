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
using System.Globalization;

namespace Pulsar.Core.Configuration
{
    /// <summary>
    /// Game configuration object. This class define the game configuration to manage in the Application Engine. <br/>
    /// </summary>
    [Serializable()]
    public sealed class GameConfiguration : ICloneable
    {
        /// <summary>
        /// Name of the current skin to use with GUI
        /// </summary>
        [Category("Skin"), Description("Define default skin to use.")]
        public string Skin { get; set; }

        /// <summary>
        /// Name of the curent skin font to use with GUI
        /// </summary>
        [Category("Skin"), Description("Define default skin font to use.")]
        public string SkinFont { get; set; }

        /// <summary>
        /// Name of the default font to load with the content manager
        /// </summary>
        [Category("Parameters"), Description("Define default font to use.")]
        public string DefaultFont { get; set; }

        /// <summary>
        /// Get or set the culture info
        /// </summary>
        [Category("Language"), Description("Define culture to use.")]
        public string Culture { get; set; }

        /// <summary>
        /// Get or set the monitor visibility
        /// </summary>
        [Category("Monitor"), Description("True if monitor is visible on startup.")]
        public bool IsMonitorVisible { get; set; }

        /// <summary>
        /// Get or set the monitor red channel color
        /// </summary>
        [Category("Monitor"), Description("Monitor red channel color.")]
        public byte MonitorColorR { get; set; }

        /// <summary>
        /// Get or set the monitor green channel color
        /// </summary>
        [Category("Monitor"), Description("Monitor green channel color.")]
        public byte MonitorColorG { get; set; }

        /// <summary>
        /// Get or set the monitor blue channel color
        /// </summary>
        [Category("Monitor"), Description("Monitor blue channel color.")]
        public byte MonitorColorB { get; set; }

        /// <summary>
        /// Get the monitor color
        /// </summary>
        [Browsable(false)]
        public Color MonitorColor
        {
            get
            {
                return new Color(this.MonitorColorR, this.MonitorColorG, this.MonitorColorB, 255);
            }
        }

        /// <summary>
        /// Create a new instance GameConfiguration
        /// </summary>
        public GameConfiguration()
            : base()
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

            return (obj is GameConfiguration) ? this.Equals((GameConfiguration)obj) : false;
        }

        /// <summary>
        /// Define if an GameConfiguration object is equals with the current object instance
        /// </summary>
        /// <param name="conf">GameConfiguration to compare</param>
        /// <returns>True if equals</returns>
        public bool Equals(GameConfiguration conf)
        {
            if (conf == null)
                return false;

            return (this.Culture == conf.Culture
                && this.Skin == conf.Skin
                && this.SkinFont == conf.SkinFont
                && this.DefaultFont == conf.DefaultFont
                && this.IsMonitorVisible == conf.IsMonitorVisible
                && this.MonitorColor == conf.MonitorColor);
        }

        /// <summary>
        /// Get the HashCode of the object
        /// </summary>
        /// <returns>HashCode of the object</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Clone the Graphics configuration instance
        /// </summary>
        /// <returns>Clone of the graphics configuration</returns>
        public object Clone()
        {
            GameConfiguration gameConfiguration = new GameConfiguration();
            gameConfiguration.Culture = this.Culture;
            gameConfiguration.DefaultFont = this.DefaultFont;
            gameConfiguration.IsMonitorVisible = this.IsMonitorVisible;
            gameConfiguration.MonitorColorR = this.MonitorColorR;
            gameConfiguration.MonitorColorG = this.MonitorColorG;
            gameConfiguration.MonitorColorB = this.MonitorColorB;
            gameConfiguration.Skin = this.Skin;
            gameConfiguration.SkinFont = this.SkinFont;

            return gameConfiguration;
        }
    }
}
