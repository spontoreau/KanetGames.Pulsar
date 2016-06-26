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

using Pulsar.Core.Display;
using Pulsar.Framework;
using SFML.Window;
using System;
using System.ComponentModel;

namespace Pulsar.Core.Configuration
{
    /// <summary>
    /// Graphics configuration object. This class define the gaphics configuration to manage in the graphics device manager.<br/>
    /// </summary>
    [Serializable()]
    public sealed class GraphicsConfiguration : ICloneable
    {
        /// <summary>
        /// Resolution rectangle of the game
        /// </summary>
        [Category("Parameters"), Description("Graphics resolution.")]
        public Resolution Resolution { get; set; }

        /// <summary>
        /// Window style
        /// </summary>
        [Category("Parameters"), Description("Window style.")]
        public Styles Styles { get; set; }

        /// <summary>
        /// Set to True if game need to be in Vertical synchronise.
        /// </summary>
        [Category("Parameters"), Description("Active Vertical synchronise.")]
        public bool IsVerticalSync { get; set; }

        /// <summary>
        /// Bits per pixel
        /// </summary>
        [Category("Parameters"), Description("Set Bit per pixel.")]
        public uint BitsPerPixel { get; set; }

        /// <summary>
        /// Set to True if the Windows OS mouse cursor need to be visible.
        /// </summary>
        [Category("Parameters"), Description("True if Windows OS mouse cursor is visible in the game.")]
        public bool IsMouseVisible { get; set; }

        /// <summary>
        /// True if the particule engine is active
        /// </summary>
        [Category("Parameters"), Description("True if particle engine is active.")]
        public bool IsParticuleEngineActive { get; set; }

        /// <summary>
        /// True if the light engine is active
        /// </summary>
        [Category("Parameters"), Description("True if light engine is active.")]
        public bool IsLightEngineActive { get; set; }

        /// <summary>
        /// Construct a new graphicsConfiguration object.
        /// </summary>
        public GraphicsConfiguration()
            : base()
        {
            Resolution = new Resolution();
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

            return (obj is GraphicsConfiguration) ? this.Equals((GraphicsConfiguration)obj) : false;
        }

        /// <summary>
        /// Define if an GraphicsConfiguration object is equals with the current object instance
        /// </summary>
        /// <param name="conf">GraphicsConfiguration to compare</param>
        /// <returns>True if equals</returns>
        public bool Equals(GraphicsConfiguration conf)
        {
            if (conf == null)
                return false;

            return (this.BitsPerPixel == conf.BitsPerPixel
                && this.Styles == conf.Styles
                && this.IsMouseVisible == conf.IsMouseVisible
                && this.IsVerticalSync == conf.IsVerticalSync
                && this.IsLightEngineActive == conf.IsLightEngineActive
                && this.IsParticuleEngineActive == conf.IsParticuleEngineActive
                && this.Resolution.Equals(conf.Resolution));
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
            GraphicsConfiguration gfxConfiguration = new GraphicsConfiguration();
            gfxConfiguration.BitsPerPixel = this.BitsPerPixel;
            gfxConfiguration.IsMouseVisible = this.IsMouseVisible;
            gfxConfiguration.IsVerticalSync = this.IsVerticalSync;
            gfxConfiguration.Resolution = this.Resolution;
            gfxConfiguration.Styles = this.Styles;
            gfxConfiguration.IsParticuleEngineActive = this.IsParticuleEngineActive;
            gfxConfiguration.IsLightEngineActive = this.IsLightEngineActive;

            return gfxConfiguration;
        }
    }
}
