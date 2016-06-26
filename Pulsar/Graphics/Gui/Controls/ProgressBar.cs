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

namespace Pulsar.Graphics.Gui.Controls
{
    /// <summary>
    /// Progress bar is a control that report a visual progression of a task.
    /// By default Progress bar are static and don't apply effect for background. user can allow effect appliment by switch the property UseEffects to true.
    /// </summary>
    public class ProgressBar : Control, IAudioable
    {
        private float _position;

        /// <summary>
        /// Over sound key properties is a key which can permit to play a sound in the audio manager
        /// </summary>
        public string OverSoundKey { get; set; }

        /// <summary>
        /// Click sound key properties is a key which can permit to play a sound in the audio manager
        /// </summary>
        public string ClickSoundKey { get; set; }

        /// <summary>
        /// Position in percent (min value is 0, max value is 1)
        /// </summary>
        public float Position
        {
            get 
            { 
                return _position; 
            }
            set
            {
                if (value > 1)
                    _position = 1;
                if (value < 0)
                    _position = 0;
                else
                    _position = value;

                BarBounds.X = Bounds.X;
                BarBounds.Y = Bounds.Y;
                BarBounds.Width = Bounds.Width * this._position;
                BarBounds.Height = Bounds.Height;
            }
        }

        /// <summary>
        /// Color of the bar
        /// </summary>
        public Color BarColor { get; set; }

        /// <summary>
        /// Bounds of the bar
        /// </summary>
        public Rectangle BarBounds { get; private set; }

        /// <summary>
        /// True if label apply effects
        /// </summary>
        public virtual bool UseEffects { get; set; }

        /// <summary>
        /// Create a new instance of the ProgressBar
        /// </summary>
        public ProgressBar()
            : base()
        {
            BarBounds = Rectangle.Empty;
        }

        /// <summary>
        /// Update the control
        /// </summary>
        /// <param name="gameTime">Time since last update</param>
        public override void Update(GameTime gameTime)
        {
            if (UseEffects)
                base.Update(gameTime);
        }
    }
}
