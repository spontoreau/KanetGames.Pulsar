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

namespace Pulsar.Graphics.Gui.Controls
{
    /// <summary>
    /// Button are Label with effects and audio capability
    /// </summary>
    public class Button : Label
    {
        /// <summary>
        /// Over sound key properties is a key which can permit to play a sound in the audio manager
        /// </summary>
        public string OverSoundKey { get; set; }

        /// <summary>
        /// Click sound key properties is a key which can permit to play a sound in the audio manager
        /// </summary>
        public string ClickSoundKey { get; set; }

        /// <summary>
        /// True if Button apply effects. Inherit from Label. Setting a value throw InvalidOperationException because button always apply effect.
        /// </summary>
        public override bool UseEffects
        {
            get
            {
                return true;
            }
            set
            {
                throw new InvalidOperationException();//todo add message
            }
        }

        /// <summary>
        /// Create a new instance of a button
        /// </summary>
        public Button()
            : base()
        {

        }
    }
}
