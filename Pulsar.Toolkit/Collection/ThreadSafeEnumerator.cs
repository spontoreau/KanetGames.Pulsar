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

using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Pulsar.Toolkit.Collections
{
    /// <summary>
    /// Thread safe typed enumerator.
    /// </summary>
    public class ThreadSafeEnumerator <T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;
        private readonly object _syncRoot;

        /// <summary>
        /// Construct a new ThreadSafe enumerator.
        /// </summary>
        /// <param name="enumerator">Typed enumerator.</param>
        /// <param name="syncRoot">Synchornized object.</param>
        public ThreadSafeEnumerator(IEnumerator<T> enumerator, object syncRoot)
        {
            this._enumerator = enumerator;
            this._syncRoot = syncRoot;

            Monitor.Enter(this._syncRoot);
        }

        /// <summary>
        /// Get the item at the current enumerator position.
        /// </summary>
        public T Current
        {
            get { return _enumerator.Current; }
        }

        /// <summary>
        /// Get the item at the current enumerator position.
        /// </summary>
        object IEnumerator.Current
        {
            get { return Current; }
        }

        /// <summary>
        /// Dispose the enumerator.
        /// </summary>
        public void Dispose()
        {
            Monitor.Exit(_syncRoot);
        }

        /// <summary>
        /// Move to next item.
        /// </summary>
        /// <returns>True if move done.</returns>
        public bool MoveNext()
        {
            return _enumerator.MoveNext();
        }

        /// <summary>
        /// Reset the enumerator.
        /// </summary>
        public void Reset()
        {
            _enumerator.Reset();
        }
    }
}
