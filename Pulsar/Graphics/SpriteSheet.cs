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
using SFML.Graphics;
using System;
using System.Collections.Generic;

namespace Pulsar.Graphics
{
    /// <summary>
    /// Define a SpriteSheet base on a texture
    /// </summary>
    [Serializable()]
    public class SpriteSheet : IDisposable
    {
        /// <summary>
        /// Internal texture to use with the sprite sheet
        /// </summary>
        [NonSerialized()]
        internal Texture Texture;

        /// <summary>
        /// Texture key of the SpriteSheet
        /// </summary>
        public string TextureContentKey { get; set; }

        /// <summary>
        /// Source rectangle in the spritesheet
        /// </summary>
        public Dictionary<string, Rectangle> SourceRectangle { get; set; }

        /// <summary>
        /// Sprite Sheet Items
        /// </summary>
        [NonSerialized()]
        private Dictionary<string, SpriteSheetItem> _items;

        /// <summary>
        /// Create a new instance of SpriteSheet
        /// </summary>
        public SpriteSheet()
        {
            this._items = new Dictionary<string, SpriteSheetItem>();
        }

        /// <summary>
        /// Internal load of the SpirteSheet
        /// </summary>
        internal void Load()
        {
            foreach (KeyValuePair<string, Rectangle> kvp in SourceRectangle)
            {
                SpriteSheetItem item = new SpriteSheetItem();
                item.SourceRectangle = kvp.Value;
                item.Texture = this.Texture;
                this._items.Add(kvp.Key, item);
            }
        }

        /// <summary>
        /// Get a Sprite item
        /// </summary>
        /// <param name="key">Key of the item</param>
        /// <returns>SpriteSheetItem</returns>
        public SpriteSheetItem Get(string key)
        {
            if (this._items.ContainsKey(key))
                return this._items[key];
            else
                throw new Exception(); //TODO
        }

        /// <summary>
        /// Dispose the SpriteSheet
        /// </summary>
        public void Dispose()
        {
            foreach (KeyValuePair<string, SpriteSheetItem> kvp in this._items)
                kvp.Value.Texture = null;

            this._items.Clear();
        }
    }
}
