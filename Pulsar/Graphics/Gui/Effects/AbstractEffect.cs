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
using Pulsar.Graphics.Gui.Effects.Interfaces;
using Pulsar.Graphics.Gui.Enums;

namespace Pulsar.Graphics.Gui.Effects
{
    /// <summary>
    /// Gui effect
    /// </summary>
    public abstract class AbstractEffect
    {
        /// <summary>
        /// Control with effect capability
        /// </summary>
        public IEffectCapable Control { get; private set; }

        /// <summary>
        /// Effect speed
        /// </summary>
        public EffectSpeedEnum Speed { get; private set; }

        /// <summary>
        /// True if effect must loop
        /// </summary>
        public bool Loop { get; private set; }

        /// <summary>
        /// Create a new instance of an Effect
        /// </summary>
        /// <param name="effectObject">Control with effect capability</param>
        /// <param name="speed">Speed of the Effect</param>
        /// <param name="loop">True if effect must loop</param>
        public AbstractEffect(IEffectCapable control, EffectSpeedEnum speed, bool loop = false)
        {
            this.Control = control;
            this.Speed = speed;
            this.Loop = loop;
        }

        /// <summary>
        /// Apply the effect on the object
        /// </summary>
        /// <param name="gameTime">Time elpase since last update</param>
        public abstract void Apply(GameTime gameTime);
    }
}
