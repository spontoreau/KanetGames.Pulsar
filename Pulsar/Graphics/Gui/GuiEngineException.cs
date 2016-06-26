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

using Pulsar.Toolkit;
using System;

namespace Pulsar.Graphics.Gui
{
    /// <summary>
    /// Define a gui exception.
    /// </summary>
    public sealed class GuiEngineException : PulsarException
    {
        /// <summary>
        /// Create a new instance of AudioEngineException.
        /// </summary>
        /// <param name="source">Source of the exception.</param>
        /// <param name="message">Message of the exception.</param>
        public GuiEngineException(string source, string message)
            : base(source, message)
        {

        }

        /// <summary>
        /// Create a new instance of GuiEngineException.
        /// </summary>
        /// <param name="source">Source of the exception.</param>
        /// <param name="message">Message of the exception.</param>
        /// <param name="innerException">Inner exception that throw the GuiEngineException.</param>
        public GuiEngineException(string source, string message, Exception innerException)
            : base(source, message, innerException)
        {

        }
    }
}
