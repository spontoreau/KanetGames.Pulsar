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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Pulsar.Toolkit.Collections
{
    /// <summary>
    /// Thread Safe Typed List.
    /// </summary>
    [Serializable()]
    public class ThreadSafeList<T> : IList<T>
    {
        private readonly List<T> _list;
        private readonly object _syncRoot = new object();

        /// <summary>
        /// Synchronized object.
        /// </summary>
        public object SyncRoot
        {
            get { return this._syncRoot; }
        }

        /// <summary>
        /// Count the number of item in the ThreadSafeList.
        /// </summary>
        public int Count
        {
            get
            {
                lock (this._syncRoot)
                {
                    return this._list.Count;
                }
            }
        }

        /// <summary>
        /// Return the item at the index.
        /// </summary>
        /// <param name="index">Index in the ThreadSafeList.</param>
        /// <returns>Item at the index.</returns>
        public T this[int index]
        {
            get
            {
                lock (this._syncRoot)
                {
                    return this._list[index];
                }
            }
            set
            {
                lock (this._syncRoot)
                {
                    this._list[index] = value;
                }
            }
        }

        /// <summary>
        /// Return true if the ThreadSafeLisrt is in read only state.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Construct a new ThreadSafeList.
        /// </summary>
        public ThreadSafeList()
        {
            this._list = new List<T>();
        }

        /// <summary>
        /// Construct a new ThreadSafeList.
        /// </summary>
        /// <param name="size">Size of the ThreadSafeList.</param>
        public ThreadSafeList(int size)
        {
            this._list = new List<T>(size);
        }

        /// <summary>
        /// Get the enumerator for the ThreadSafeList.
        /// </summary>
        /// <returns></returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            lock (this._syncRoot)
            {
                return new ThreadSafeEnumerator<T>(this._list.GetEnumerator(), this._syncRoot);
            }
        }

        /// <summary>
        /// Get the ThreadSafeList enumerator.
        /// </summary>
        /// <returns>An enumerator object.</returns>
        public IEnumerator GetEnumerator()
        {
            lock (this._syncRoot)
            {
                return new ThreadSafeEnumerator<T>(_list.GetEnumerator(), this._syncRoot);
            }
        }

        /// <summary>
        /// Add the item from the ThreadSafeList.
        /// </summary>
        /// <param name="item">Item to add.</param>
        public void Add(T item)
        {
            lock (this._syncRoot)
            {
                this._list.Add(item);
            }
        }

        /// <summary>
        /// Remove all item in the ThreadSafeList.
        /// </summary>
        public void Clear()
        {
            lock (this._syncRoot)
            {
                this._list.Clear();
            }
        }

        /// <summary>
        /// Return True if the ThreadSafeList contains the item.
        /// </summary>
        /// <param name="item">Item to test.</param>
        /// <returns>True if the ThreadSafeList contains the item.</returns>
        public bool Contains(T item)
        {
            lock (this._syncRoot)
            {
                return this._list.Contains(item);
            }
        }

        /// <summary>
        /// Copy the ThreadSafeList in a unidimentional array. <br/>
        /// Copy begin at the index.
        /// </summary>
        /// <param name="array">The array which get the copy item.</param>
        /// <param name="arrayIndex">Begin index for the copy.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (this._syncRoot)
            {
                this._list.CopyTo(array, arrayIndex);
            }
        }

        /// <summary>
        /// Remove the item from the ThreadSafeList.
        /// </summary>
        /// <param name="item">Item to remove.</param>
        /// <returns>True if the item was remove.</returns>
        public bool Remove(T item)
        {
            lock (this._syncRoot)
            {
                return this._list.Remove(item);
            }
        }

        /// <summary>
        /// Return the first index occurence of an item.
        /// </summary>
        /// <param name="item">Item to find.</param>
        /// <returns>Index of the item in the ThreadSafeList.</returns>
        public int IndexOf(T item)
        {
            lock (this._syncRoot)
            {
                return this._list.IndexOf(item);
            }
        }

        /// <summary>
        /// Insert an item in the ThreadSafeList at the index.
        /// </summary>
        /// <param name="index">Index of the item.</param>
        /// <param name="item">Item to insert.</param>
        public void Insert(int index, T item)
        {
            lock (this._syncRoot)
            {
                this._list.Insert(index, item);
            }
        }

        /// <summary>
        /// Remove the item corresponding to the index.
        /// </summary>
        /// <param name="index">Index of the item to remove.</param>
        public void RemoveAt(int index)
        {
            lock (this._syncRoot)
            {
                this._list.RemoveAt(index);
            }
        }

        /// <summary>
        /// Return the ThreadSafeList in a ReadOnlyCollection.
        /// </summary>
        /// <returns>ReadOnlyCollection representing the ThreadSafeList.</returns>
        public ReadOnlyCollection<T> AsReadOnly()
        {
            lock (this._syncRoot)
            {
                return new ReadOnlyCollection<T>(this);
            }
        }

        /// <summary>
        /// Foreach implementation for the ThreadSafeList.
        /// </summary>
        /// <param name="action">Action object.</param>
        public void ForEach(Action<T> action)
        {
            lock (this._syncRoot)
            {
                foreach (var item in _list)
                {
                    action(item);
                }
            }
        }

        /// <summary>
        /// Return true if predicate is match.
        /// </summary>
        /// <param name="match">Predicate to match.</param>
        /// <returns>True if predicate is match.</returns>
        public bool Exists(Predicate<T> match)
        {
            lock (this._syncRoot)
            {
                foreach (var item in _list)
                {
                    if (match(item))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
