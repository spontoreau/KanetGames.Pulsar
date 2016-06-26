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

using Pulsar.Core.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Pulsar.Core.Configuration
{
    /// <summary>
    /// HotKey configuration object. This class define the HotKey to manage in the InputManager. <br/>
    /// </summary>
    [Serializable()]
    public sealed class HotKeyConfiguration
    {
        /// <summary>
        /// Hot keys list
        /// </summary>
        [Category("Parameters"), Description("Define hot keys.")]
        public List<HotKey> HotKeys { get; set; }

        /// <summary>
        /// True if hotkeys are enabled and can be use with the HotKey Manager
        /// </summary>
        [Browsable(false)]
        public bool IsEnabled
        {
            get
            {
                return (HotKeys.Count > 0);
            }
        }

        /// <summary>
        /// Construct a new HotKeyConfiguration object.
        /// </summary>
        public HotKeyConfiguration()
        {
            this.HotKeys = new List<HotKey>();
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

            return (obj is HotKeyConfiguration) ? this.Equals((HotKeyConfiguration)obj) : false;
        }

        /// <summary>
        /// Define if an GraphicsConfiguration object is equals with the current object instance
        /// </summary>
        /// <param name="conf">GraphicsConfiguration to compare</param>
        /// <returns>True if equals</returns>
        public bool Equals(HotKeyConfiguration conf)
        {
            if (conf == null)
                return false;

            if(HotKeys.Count != conf.HotKeys.Count)
                return false;

            for (int i = 0 , l = HotKeys.Count; i < l; i++)
            {
                if (!HotKeys[i].Equals(conf.HotKeys[i]))
                    return false;
            }

            return true;
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
