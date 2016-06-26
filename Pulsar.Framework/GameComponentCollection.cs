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

namespace Pulsar.Framework
{
    /// <summary>
    /// A collection of game components.
    /// </summary>
    public sealed class GameComponentCollection : Collection<IGameComponent>
    {
        /// <summary>
        /// Raised when a component is added to the GameComponentCollection.
        /// </summary>
        public event EventHandler<GameComponentCollectionEventArgs> ComponentAdded;

        /// <summary>
        /// Raised when a component is removed from the GameComponentCollection.
        /// </summary>
        public event EventHandler<GameComponentCollectionEventArgs> ComponentRemoved;

        /// <summary>
        /// Inserts a GameComponent into the GameComponentCollection at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which GameComponent should be inserted.</param>
        /// <param name="item">The GameComponent to insert.</param>
        protected override void InsertItem(int index, IGameComponent item)
        {
            base.InsertItem(index, item);
            if (item != null)
                OnComponentAdded(new GameComponentCollectionEventArgs(item));
        }

        /// <summary>
        /// Removes all GameComponents from the GameComponentCollection
        /// </summary>
        protected override void ClearItems()
        {
            foreach(IGameComponent component in this)
                OnComponentRemoved(new GameComponentCollectionEventArgs(component));
            base.ClearItems();
        }

        /// <summary>
        /// Removes the GameComponent at the specified index of the GameComponentCollection.
        /// </summary>
        /// <param name="index">The zero-based index of the GameComponent to remove.</param>
        protected override void RemoveItem(int index)
        {
            IGameComponent component = this[index];
            base.RemoveItem(index);
            if(component != null)
                OnComponentRemoved(new GameComponentCollectionEventArgs(component));
        }

        /// <summary>
        /// Called when a GameComponent was add. Raises the ComponentAdded event.
        /// </summary>
        /// <param name="eventArgs">Arguments to the ComponentAdded event.</param>
        private void OnComponentAdded(GameComponentCollectionEventArgs eventArgs)
        {
            EventHandler<GameComponentCollectionEventArgs> tmp = ComponentAdded;
            if (tmp != null)
                tmp(this, eventArgs);
        }

        /// <summary>
        /// Called when a GameComponent was remove. Raises the ComponentRemoved event.
        /// </summary>
        /// <param name="eventArgs">Arguments to the ComponentRemoved event.</param>
        private void OnComponentRemoved(GameComponentCollectionEventArgs eventArgs)
        {
            EventHandler<GameComponentCollectionEventArgs> tmp = ComponentRemoved;
            if (tmp != null)
                tmp(this, eventArgs);
        }
    }
}
