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

namespace Pulsar.Framework
{
    /// <summary>
    /// Snapshot of the game timing state expressed in values that can be used by variable-step (real time) or fixed-step (game time) games. 
    /// </summary>
    public sealed class GameTime
    {
        /// <summary>
        /// The amount of elapsed game time since the last update.
        /// </summary>
        public TimeSpan TotalGameTime { get; internal set; }

        /// <summary>
        /// The amount of game time since the start of the game.
        /// </summary>
        public TimeSpan ElapsedGameTime { get; internal set; }

        /// <summary>
        /// Creates a new instance of GameTime.
        /// </summary>
        public GameTime()
            :this(TimeSpan.Zero, TimeSpan.Zero)
        {

        }

        /// <summary>
        /// Creates a new instance of GameTime.
        /// </summary>
        /// <param name="totalGameTime">The amount of game time since the start of the game.</param>
        /// <param name="elapsedGameTime">The amount of elapsed game time since the last update.</param>
        public GameTime(TimeSpan totalGameTime, TimeSpan elapsedGameTime)
        {
            this.TotalGameTime = totalGameTime;
            this.ElapsedGameTime = elapsedGameTime;
        }
    }
}
