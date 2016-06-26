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

namespace Pulsar.Director.Events
{
    /// <summary>
    /// Load Started event arguments
    /// </summary>
    public class LoadStartedEventArgs : EventArgs
    {
        /// <summary>
        /// Total number of Shot to load
        /// </summary>
        public int ShotNumber { get; private set; }

        /// <summary>
        /// True if the Scene is a heavy load
        /// </summary>
        public bool IsHeavyLoad { get; private set; }

        /// <summary>
        /// Create a new instance of LoadStartedEventArgs
        /// </summary>
        /// <param name="shotNumber">Total number of Shot to load</param>
        /// <param name="isHeavyLoad">True if the Scene is a heavy load</param>
        public LoadStartedEventArgs(int shotNumber, bool isHeavyLoad)
        {
            this.ShotNumber = shotNumber;
            this.IsHeavyLoad = isHeavyLoad;
        }
    }
}
