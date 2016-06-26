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

using System.Collections.Generic;

namespace Pulsar.Core.Commands
{
    public sealed class CommandHistory : List<string>
    {
        private int _currentIndex;

        public string Next()
        {
            if (this.Count == 0)
            {
                return string.Empty;
            }
            else
            {
                return (this._currentIndex + 1 > this.Count - 1) ? this[Count - 1] : this[++this._currentIndex]; 
            }
        }

        public string Previous()
        {
            if (this.Count == 0)
            {
                return string.Empty;
            }
            else
            {
                return (this._currentIndex - 1 < 0) ? this[0] : this[--this._currentIndex];
            }
        }

        public void Add(string command)
        {
            if (!string.IsNullOrEmpty(command))
                base.Add(command);

            this._currentIndex = this.Count;
        }

        public void ClearHistory()
        {
            base.Clear();
            this._currentIndex = 0;
        }
    }
}
