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
using System.IO;
using System.IO.Compression;

namespace Pulsar.Toolkit.Helpers
{
    /// <summary>
    /// ZipHelper for stream compression and uncompression operation.
    /// </summary>
    public static class ZipHelper
    {
        /// <summary>
        /// Compress a stream.
        /// </summary>
        /// <param name="stream">The stream to compress.</param>
        /// <returns>Byte array representation of the compressed stream.</returns>
        public static byte[] Compress(Stream stream)
        {
            GZipStream gZipStream = null;
            byte[] compressByteArray = null;
            MemoryStream outStream = new MemoryStream();
            try
            {
                byte[] buffer = new byte[stream.Length];
                int bufferRead = 0;

                gZipStream = new GZipStream(outStream, CompressionMode.Compress);

                bufferRead = stream.Read(buffer, 0, buffer.Length);
                gZipStream.Write(buffer, 0, bufferRead);
                gZipStream.Close();
                compressByteArray = outStream.ToArray();
            }
            catch (Exception ex)
            {
                throw new PulsarException(typeof(ZipHelper).FullName, Pulsar.Toolkit.Resources.Exceptions.Helper.CompressFailed, ex);
            }
            finally
            {
                if (gZipStream != null)
                {
                    gZipStream.Close();
                }

                if (outStream != null)
                {
                    outStream.Close();
                }
            }

            return compressByteArray;
        }

        /// <summary>
        /// Uncompress a stream.
        /// </summary>
        /// <param name="stream">The stream to uncompress.</param>
        /// <returns>Byte array representation of the uncompressed stream.</returns>
        public static byte[] Uncompress(Stream stream)
        {
            GZipStream gZipStream = null;
            int bufferLen = 1024;
            int numberRead = 0;

            byte[] decompressByteArray = new byte[bufferLen];
            MemoryStream decompressedStream = new MemoryStream();

            byte[] resultByteArray = null;

            try
            {
                gZipStream = new GZipStream(stream, CompressionMode.Decompress);
                do
                {
                    numberRead = gZipStream.Read(decompressByteArray, 0, decompressByteArray.Length);
                    decompressedStream.Write(decompressByteArray, 0, numberRead);
                }
                while (numberRead > 0);

                gZipStream.Close();

                resultByteArray = decompressedStream.ToArray();
            }
            catch (Exception ex)
            {
                throw new PulsarException(typeof(ZipHelper).FullName, Pulsar.Toolkit.Resources.Exceptions.Helper.UncompressFailed, ex);
            }
            finally
            {
                if (gZipStream != null)
                {
                    gZipStream.Close();
                }
            }

            return resultByteArray;
        }
    }
}
