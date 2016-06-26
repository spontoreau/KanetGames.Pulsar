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
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Pulsar.Toolkit.Repository
{
    /// <summary>
    /// Abstract repository
    /// </summary>
    /// <typeparam name="T">Type to manage with the repository</typeparam>
    public abstract class AbstractRepository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// The Data Context
        /// </summary>
        public IDataContext Context { get; protected set; }

        /// <summary>
        /// Get all entity of T type
        /// </summary>
        /// <returns>List of T type</returns>
        public abstract List<T> GetAll();

        /// <summary>
        /// Get an entity of T type
        /// </summary>
        /// <param name="filter">Filter for get the entity</param>
        /// <returns></returns>
        public abstract T Get(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Insert an entity
        /// </summary>
        /// <param name="entity">The entity to insert</param>
        public abstract void Insert(T entity);

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entity">The entity to insert</param>
        public abstract void Update(T entity);

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        public abstract void Delete(T entity);
        
        /// <summary>
        /// Dispose the repository
        /// </summary>
        public void Dispose()
        {
            if (this.Context != null)
                this.Context.Dispose();
        }
    }
}
