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

namespace Pulsar.Toolkit.Collections
{
    /// <summary>
    /// Thread safe typed enumerable.
    /// </summary>
    public class ThreadSafeEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _enumerable;
        private readonly object _syncRoot;

        /// <summary>
        /// Construct a new ThreadSafeEnumerable object.
        /// </summary>
        /// <param name="enumerable">Inner enumerable.</param>
        /// <param name="syncRoot">Synchronized object.</param>
        public ThreadSafeEnumerable(IEnumerable<T> enumerable, object syncRoot)
        {
            this._syncRoot = syncRoot;
            this._enumerable = enumerable;
        }

        /// <summary>
        /// Get an enumerator for the type T.
        /// </summary>
        /// <returns>IEnumerator for the type T.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new ThreadSafeEnumerator<T>(this._enumerable.GetEnumerator(), this._syncRoot);
        }

        /// <summary>
        /// Get an enumerator.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}