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

namespace Pulsar.Graphics.Gui.Controls
{
    public class CheckBox : Control, IAudioable
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
        /// True if the checkbox is check
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// Checked color
        /// </summary>
        public Color CheckedColor { get; set; }

        /// <summary>
        /// Unchecked color
        /// </summary>
        public Color UncheckedColor { get; set; }

        /// <summary>
        /// Create a new instance of a checkbox
        /// </summary>
        public CheckBox()
            : base()
        {

        }

        /// <summary>
        /// Method for raising the click event.
        /// </summary>
        protected internal override void OnClick(MouseButtonEventArgs e)
        {
            this.Checked = !this.Checked;
            this.BackgroundColor = (this.Checked) ? this.CheckedColor : this.UncheckedColor;
            base.OnClick(e);
        }
    }
}
