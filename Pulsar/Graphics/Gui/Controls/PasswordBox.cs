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

namespace Pulsar.Graphics.Gui.Controls
{
    /// <summary>
    /// Password box
    /// </summary>
    public class PasswordBox : TextBox
    {
        internal const string PASSWORD_CHAR = "*";//TODO add to resource file

        /// <summary>
        /// Create a new instance of PasswordBox
        /// </summary>
        public PasswordBox()
            : base()
        {

        }

        /// <summary>
        /// Add char in the textbox
        /// </summary>
        /// <param name="c">Char to add</param>
        protected internal override void Add(string c)
        {
            this.Value += c;
            this.Text += PASSWORD_CHAR;
        }

        /// <summary>
        /// Compute backspace
        /// </summary>
        protected internal override void Backspace()
        {
            this.Value = this.Value.Substring(0, this.Value.Length - 1);
            this.Text = this.Text.Substring(0, this.Text.Length - 1);
        }
    }
}
