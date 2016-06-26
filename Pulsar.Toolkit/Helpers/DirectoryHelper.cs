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

using System.IO;

namespace Pulsar.Toolkit
{
    /// <summary>
    /// Helper lass for directory
    /// </summary>
    public static class DirectoryHelper
    {
        /// <summary>
        /// Check if Directory is avaible
        /// <param name="rootDirectory">Directory to check</param>
        /// </summary>
        public static void Check(string directory)
        {
            if (!string.IsNullOrEmpty(directory))
            {
                if (!Directory.Exists(directory))
                {
                    throw new PulsarException(typeof(DirectoryHelper).FullName, string.Format(Pulsar.Toolkit.Resources.Exceptions.Helper.RootDirectoryNotExist, directory));
                }
            }
            else
            {
                throw new PulsarException(typeof(DirectoryHelper).FullName, string.Format(Pulsar.Toolkit.Resources.Exceptions.Helper.RootDirectoryEmpty, directory));
            }
        }
    }
}
