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

using Pulsar.Framework;
using System;
using System.ComponentModel;
using System.Reflection;

namespace Pulsar.Core.Display
{
    /// <summary>
    /// Resolution.
    /// </summary>
    [Serializable()]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public sealed class Resolution
    {
        [NonSerialized()]
        private Rectangle _bounds = Rectangle.Empty;

        private float _width;
        private float _height;

        /// <summary>
        /// Bounds of the resolution.
        /// </summary>
        [Browsable(false)]
        public Rectangle Bounds
        {
            get 
            { 
                return this._bounds; 
            }
        }

        /// <summary>
        /// Width of the resolution.
        /// </summary>
        [Description("Resolution width")]
        public float Width
        {
            get
            {
                return this._width;
            }
            set
            {
                this._width = value;
                this._bounds.Width = this._width;
            }
        }

        /// <summary>
        /// Height of the resolution.
        /// </summary>
        [Description("Resolution height")]
        public float Height
        {
            get 
            { 
                return this._height; 
            }
            set
            {
                this._height = value;
                this._bounds.Height = this._height;
            }
        }

        /// <summary>
        /// Create a new instance of Resolution.
        /// </summary>
        public Resolution()
        {
            this._bounds = Rectangle.Empty;
        }

        /// <summary>
        /// Create a new instance of Resolution.
        /// </summary>
        /// <param name="width">width of the resolution.</param>
        /// <param name="height">height of the resolution.</param>
        public Resolution(float width, float height)
        {
            this._width = width;
            this._height = height;
            this._bounds = new Rectangle(0f, 0f, width, height);
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

            return (obj is Resolution) ? this.Equals((Resolution)obj) : false;
        }

        /// <summary>
        /// Define if an Resolution object is equals with the current object instance
        /// </summary>
        /// <param name="reso">Resolution to compare</param>
        /// <returns>True if equals</returns>
        public bool Equals(Resolution reso)
        {
            if (reso == null)
                return false;

            return (this.Bounds.Equals(reso.Bounds)
                && this.Height == reso.Height
                && this.Width == reso.Width);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// String representation of a Resolution.
        /// </summary>
        /// <returns>String representation of a Resolution.</returns>
        public override string ToString()
        {
            return this._bounds.Width + "x" + this._bounds.Height;
        }

        /// <summary>
        /// Parse a VirtualResolutionEnum into a Resolution object
        /// </summary>
        /// <param name="virtualResolution"></param>
        /// <returns></returns>
        public static Resolution Parse(VirtualResolutionEnum virtualResolution)
        {
            Type type = virtualResolution.GetType();
            MemberInfo[] member = type.GetMember(virtualResolution.ToString());
            object[] attributes = member[0].GetCustomAttributes(typeof(ResolutionAttribute), false);
            ResolutionAttribute resolutionAttribute = (ResolutionAttribute)attributes[0];
            return new Resolution(resolutionAttribute.Width, resolutionAttribute.Height);
        }
    }
}
