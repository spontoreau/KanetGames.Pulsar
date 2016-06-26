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

namespace Pulsar.Core.Culture
{
    /// <summary>
    /// Define a language
    /// </summary>
    [Serializable()]
    public sealed class Language : IDisposable
    {
        /// <summary>
        /// Name of the language
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Code of the language
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// List of translation of function
        /// </summary>
        public List<FunctionTranslation> Functions { get; set; }

        /// <summary>
        /// Create a new instance of screen translation
        /// </summary>
        public Language()
        {
            Functions = new List<FunctionTranslation>();
        }

        /// <summary>
        /// Dispose the language
        /// </summary>
        public void Dispose()
        {
            foreach (FunctionTranslation ft in Functions)
                ft.Dispose();

            this.Functions.Clear();
        }

        /// <summary>
        /// Define if an object is equals with the current object instance
        /// </summary>
        /// <param name="obj">Obj to compare</param>
        /// <returns>True if equals</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            return (obj is Language) ? this.Equals((Language)obj) : false;
        }

        /// <summary>
        /// Define if a Language object is equals with the current object instance
        /// </summary>
        /// <param name="conf">Language to compare</param>
        /// <returns>True if equals</returns>
        public bool Equals(Language lang)
        {
            if (lang == null)
                return false;

            if (this.Key == lang.Key && this.Name == lang.Name && this.Functions.Count == lang.Functions.Count)
            {
                for (int i = 0, l = this.Functions.Count; i < l; i++)
                {
                    if (!this.Functions[i].Equals(lang.Functions[i]))
                        return false;
                }

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Get the HashCode of the object
        /// </summary>
        /// <returns>HashCode of the object</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
