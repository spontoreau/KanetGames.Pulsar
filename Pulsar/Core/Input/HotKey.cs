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

using SFML.Window;
using System;

namespace Pulsar.Core.Input
{
    /// <summary>
    /// HotKey entity
    /// </summary>
    [Serializable()]
    public sealed class HotKey
    {
        /// <summary>
        /// HotKey action to made
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// True if shift pressed need
        /// </summary>
        public bool IsShift { get; set; }

        /// <summary>
        /// True if control pressed need
        /// </summary>
        public bool IsCtrl { get; set; }

        /// <summary>
        /// True if alt pressed need
        /// </summary>
        public bool IsAlt { get; set; }

        /// <summary>
        /// Key to released for the HotKey
        /// </summary>
        public Keyboard.Key Key { get; set; }

        /// <summary>
        /// Create a new instance of HotKey
        /// </summary>
        public HotKey()
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

            return (obj is HotKey) ? this.Equals((HotKey)obj) : false;
        }

        /// <summary>
        /// Define if an HotKey object is equals with the current object instance
        /// </summary>
        /// <param name="hk">HotKey to compare</param>
        /// <returns>True if equals</returns>
        public bool Equals(HotKey hk)
        {
            if (hk == null)
                return false;

            return (this.Action == hk.Action
                && this.IsAlt == hk.IsAlt
                && this.IsCtrl == hk.IsCtrl
                && this.IsShift == hk.IsShift
                && this.Key == hk.Key);
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
