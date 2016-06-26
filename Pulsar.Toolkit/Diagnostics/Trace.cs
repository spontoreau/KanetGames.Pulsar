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

namespace Pulsar.Toolkit.Diagnostics
{
    /// <summary>
    /// Entity corresponding to a logger trace.
    /// </summary>
    [Serializable]
    public class Trace
    {
        /// <summary>
        /// Source of the trace.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Message of the trace.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// State of the trace.
        /// </summary>
        public TraceState State { get; set; }

        /// <summary>
        /// Create a new instance of Trace.
        /// </summary>
        public Trace()
        {

        }

        /// <summary>
        /// Create a new instance of Trace.
        /// </summary>
        /// <param name="source">Source of the trace.</param>
        /// <param name="message">Message of the trace.</param>
        /// <param name="state">State of the trace.</param>
        public Trace(string source, string message, TraceState state)
        {
            this.Source = source;
            this.Message = message;
            this.State = state;
        }


    }
}
