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
using SFML.Window;

namespace Pulsar.Graphics.Gui.Controls
{
    /// <summary>
    /// Track bar
    /// </summary>
    public class TrackBar : Control, IAudioable
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
            get { return _position; }
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
        /// Create a new instance of the TrackBar
        /// </summary>
        public TrackBar()
            : base()
        {
            BarBounds = Rectangle.Empty;
        }

        protected internal override void OnClick(MouseButtonEventArgs args)
        {
            //Only change position if it's mouse left button
            this._position = (args.X - this.Bounds.X) / this.Bounds.X;
            this.BarBounds.X = this.Bounds.X;
            this.BarBounds.Y = this.Bounds.Y;
            this.BarBounds.Width = this.Bounds.Width * this._position;
            this.BarBounds.Height = this.Bounds.Height;
            base.OnClick(args);
        }
    }
}
