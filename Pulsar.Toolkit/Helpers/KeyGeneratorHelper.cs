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
using System.Text;

namespace Pulsar.Toolkit.Helpers
{
    /// <summary>
    /// Helper for generate random keys
    /// </summary>
    public static class KeyGeneratorHelper
    {
        /// <summary>
        /// Generate a random Token key.
        /// </summary>
        /// <param name="tokenSize">String size of token.</param>
        /// <returns>The token.</returns>
        public static string GetToken(int tokenSize)
        {
            if (tokenSize <= 0)
                throw new PulsarException(typeof(KeyGeneratorHelper).FullName, Pulsar.Toolkit.Resources.Exceptions.Helper.TokenLenghtIncorrect);

            char[] finalChar = new char[tokenSize];
            char[] tokenChar = Pulsar.Toolkit.Resources.Format.Helper.TokenCharArray.ToCharArray();

            Random r = new Random();

            for (int i = 0; i < tokenSize; i++)
                finalChar[i] = tokenChar[r.Next(tokenChar.Length)];

            return new string(finalChar);


        }

        /// <summary>
        /// Generate a new Guid.
        /// </summary>
        /// <returns>The Guid.</returns>
        public static string GetGuid()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Get a random 24 bytes key.
        /// </summary>
        /// <returns>Bytes array corresponding to the key.</returns>
        public static byte[] Get24Bytes()
        {
            return Encoding.ASCII.GetBytes(GetToken(24));
        }

        /// <summary>
        /// get a random 8 bytes key.
        /// </summary>
        /// <returns>Bytes array corresponding to the key.</returns>
        public static byte[] Get8Bytes()
        {
            return Encoding.ASCII.GetBytes(GetToken(8));
        }
    }
}
