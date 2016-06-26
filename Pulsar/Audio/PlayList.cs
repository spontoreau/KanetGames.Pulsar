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
using System.Collections.ObjectModel;
using System.Linq;

namespace Pulsar.Audio
{
    /// <summary>
    /// Collection of song
    /// </summary>
    [Serializable()]
    public sealed class PlayList
    {
        private Collection<Song> _collection;

        /// <summary>
        /// Get the songs collection
        /// </summary>
        public Collection<Song> Songs
        {
            get
            {
                return this._collection;
            }
        }

        /// <summary>
        /// Create a new instance of PlayList
        /// </summary>
        public PlayList()
        {
            this._collection = new Collection<Song>();
        }
        /// <summary>
        /// Name of the play list
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get the song corresponding to a key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Song this[string key]
        {
            get
            {
                return (from s in this._collection where s.Key == key select s).FirstOrDefault();
            }
        }

        /// <summary>
        /// Add song to the playlist
        /// </summary>
        /// <param name="song">Song to add</param>
        public void Add(Song song)
        {
            this._collection.Add(song);
        }

        /// <summary>
        /// Remove song from the playlist
        /// </summary>
        /// <param name="song">Song to remove</param>
        public void Remove(Song song)
        {
            this._collection.Add(song);
        }
    }
}
