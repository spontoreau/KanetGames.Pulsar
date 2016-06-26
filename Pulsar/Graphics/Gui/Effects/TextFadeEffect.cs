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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsar.Graphics.Gui.Effects
{
    /// <summary>
    /// Text fade effect
    /// </summary>
    public class TextFadeEffect : FadeEffect
    {
        /// <summary>
        /// Create a new instance of a TextFadeEffect
        /// </summary>
        /// <param name="controle">Gui controle</param>
        /// <param name="speed">Speed of the Effect</param>
        /// <param name="from">From value of the Effect</param>
        /// <param name="to">To value of the Effect</param>
        /// <param name="loop">True if effect must loop</param>
        public TextFadeEffect(ITextFadeCapable controle, EffectSpeedEnum speed, float from, float to, bool loop = false)
            : base(controle, speed, from, to, loop)
        {

        }

        /// <summary>
        /// Apply the effect on the object
        /// </summary>
        /// <param name="gameTime">Time elpase since last update</param>
        public override void Apply(GameTime gameTime)
        {
            if (this.Control != null)
            {
                ITextFadeCapable fadeControl = (ITextFadeCapable)this.Control;

                //Apply text fade effect
                fadeControl.TextFade =
                    (fadeControl.FadeIn) ?
                    MathHelper.Min(fadeControl.TextFade + ((float)gameTime.ElapsedGameTime.TotalSeconds * (int)this.Speed), this._to) :
                    MathHelper.Max(fadeControl.TextFade - ((float)gameTime.ElapsedGameTime.TotalSeconds * (int)this.Speed), this._from);

                if (this.Loop && fadeControl.TextFade >= this._to)//if we loop and control fade is equal or better than the fade reach, set fade to start value
                {
                    fadeControl.TextFade = this._from;
                }
            }
        }
    }
}
