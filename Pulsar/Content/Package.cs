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

using Pulsar.Toolkit.Helpers;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace Pulsar.Content
{
    /// <summary>
    /// Define a compress package of texture
    /// </summary>
    [Serializable()]
    public sealed class Package
    {
        /// <summary>
        /// Key to use in the content manager
        /// </summary>
        public string ContentKey { get; set; }

        /// <summary>
        /// Package type
        /// </summary>
        public PackageType Type { get; set; }

        /// <summary>
        /// Pixel compress byte array representing the texture in the pakage
        /// </summary>
        public byte[] ContentData { get; set; }

        /// <summary>
        /// Create a new instance of TexturePackage
        /// </summary>
        /// <param name="key">Key to use in the content manager</param>
        /// <param name="texture">Texture to include in the package</param>
        public Package(string key, string texturePath)
        {
            this.ContentKey = key;

            Stream stream = File.OpenRead(texturePath);
            this.ContentData = ZipHelper.Compress(stream);
            stream.Close();
        }
    }
}
