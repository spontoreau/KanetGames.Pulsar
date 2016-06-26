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

using Pulsar.Core.Culture;
using Pulsar.Toolkit.Helpers;
using System.Collections.Generic;

namespace Pulsar.Content.Resolvers
{
    /// <summary>
    /// Resolver for Culture file type
    /// </summary>
    public sealed class CultureContentResolver : ContentResolver
    {
        /// <summary>
        /// Supported file format for font
        /// </summary>
        private List<string> _supportFormat = new List<string> { Pulsar.Resources.Format.ContentManager.Xml, Pulsar.Resources.Format.ContentManager.Lng };

        /// <summary>
        /// Load a content from a asset file name
        /// </summary>
        /// <param name="assetFileName">Asset name, relative to the loader root directory, and including the file extension.</param>
        /// <returns>Return a Font instance corresponding to the asset file name</returns>
        public override object Load(string assetFileName)
        {
            Language language = null;

            if (assetFileName.Length > 4 && assetFileName[assetFileName.Length - 4] == '.') // need to have 5 char mini x.xxx and a point before the format suffix.
            {
                //get the 4 last char
                string fileFormat = assetFileName.Substring(assetFileName.Length - 3, 3).ToUpper();

                if (this._supportFormat.Contains(fileFormat))
                {
                    try
                    {
                        language = SerializerHelper.Load<Language>(assetFileName);
                    }
                    catch
                    {
                        throw;
                    }
                }
                else
                {
                    throw new ContentLoadException(typeof(CultureContentResolver).FullName, string.Format(Pulsar.Resources.Exceptions.ContentManager.FormatNotSupportByResolver, assetFileName, typeof(CultureContentResolver).FullName));
                }
            }
            else
            {
                throw new ContentLoadException(typeof(CultureContentResolver).FullName, string.Format(Pulsar.Resources.Exceptions.ContentManager.FileNameIncorrect, assetFileName));
            }

            return language;
        }
    }
}
