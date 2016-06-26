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
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Pulsar.Toolkit.Security
{
    /// <summary>
    /// Cipher class. Can perform SHA256 encrypt and TripleDES encryt/decrypt.
    /// </summary>
    public sealed class Cipher
    {
        /// <summary>
        /// Cipher type.
        /// </summary>
        private CipherType Type { get; set; }

        /// <summary>
        /// IV parameter for TripleDES operation.
        /// </summary>
        private byte[] IV { get; set; }

        /// <summary>
        /// Key for TripleDES operation.
        /// </summary>
        private byte[] Key { get; set; }

        /// <summary>
        /// Create a new instance of Cipher.
        /// </summary>
        /// <param name="type">Cipher type.</param>
        /// <param name="IV">Optional. IV parameter for TripleDES operation.</param>
        /// <param name="key">Optional. Key parameter for TripleDES operation.</param>
        public Cipher(CipherType type, byte[] IV = null, byte[] key = null)
        {
            this.Type = type;
            this.IV = IV;
            this.Key = key;
        }

        /// <summary>
        /// Convert a string in byte array.
        /// </summary>
        /// <param name="str">String to convert.</param>
        /// <returns>Byte array representation of the string.</returns>
        private byte[] ConvertToBytes(string str)
        {
            if (this.Type == CipherType.SHA256)
                return Encoding.UTF8.GetBytes(str);
            else
            {
                return Encoding.Default.GetBytes(str);
            }
        }

        /// <summary>
        /// Convert a byte array in string.
        /// </summary>
        /// <param name="bytes">Byte array to convert.</param>
        /// <returns>String representation of the byte array.</returns>
        private string ConvertToString(byte[] bytes)
        {
            if (this.Type == CipherType.SHA256)
                return Convert.ToBase64String(bytes);
            else
            {
                StringBuilder sb = new StringBuilder();

                foreach (byte b in bytes)
                {
                    sb.Append(string.Format("{0:x2}", (uint)System.Convert.ToUInt32(b.ToString())));
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Encrypt a string.
        /// </summary>
        /// <param name="str">String to encrypt.</param>
        /// <returns>Ciphered string.</returns>
        public string Encrypt(string str)
        {
            string result = string.Empty;

            try
            {
                byte[] byteArraySource = ConvertToBytes(str);
                byte[] byteArrayResult = Encrypt(ref byteArraySource);
                result = ConvertToString(byteArrayResult);
            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Decrypt a ciphered string.
        /// </summary>
        /// <param name="str">Chiphered string.</param>
        /// <returns>Clear string.</returns>
        public string Decrypt(string str)
        {
            string result = string.Empty;

            try
            {
                byte[] byteArraySource = Enumerable.Range(0, str.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(str.Substring(x, 2), 16)).ToArray();
                byte[] byteArrayResult = Decrypt(ref byteArraySource);
                result = new string(Encoding.Default.GetChars(byteArrayResult));
            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Encrypt a bit array.
        /// </summary>
        /// <param name="byteArray">Byte array to encrypt.</param>
        /// <returns>Bit array ciphered.</returns>
        public byte[] Encrypt(ref byte[] byteArray)
        {
            byte[] result = null;
            if (Type == CipherType.TripleDES)
            {
                if(IsTripleCapable())
                {
                    ICryptoTransform transform = null;
                    try
                    {
                        transform = GetTripleDES().CreateEncryptor();
                        result = transform.TransformFinalBlock(byteArray, 0, byteArray.Length);
                    }
                    catch(Exception ex)
                    {
                        throw new PulsarException(typeof(Cipher).FullName, Pulsar.Toolkit.Resources.Exceptions.Cipher.TripleEncryptFailed, ex);
                    }
                    finally
                    {
                        if(transform != null)
                            transform.Dispose();
                    }
                }
                else
                {
                    ThrowTripleParamException();
                }
            }
            else
            {
                SHA256Managed sha = null;

                try
                {
                    sha = new SHA256Managed();
                    result = sha.ComputeHash(byteArray);
                }
                catch (Exception ex)
                {
                    throw new PulsarException(typeof(Cipher).FullName, Pulsar.Toolkit.Resources.Exceptions.Cipher.SHA256EncryptFailed, ex);
                }
                finally
                {
                    if (sha != null)
                        sha.Dispose();
                }
            }

            return result;
        }

        /// <summary>
        /// Decrypt a bit array.
        /// </summary>
        /// <param name="byteArray">Byte array to decrypt.</param>
        /// <returns>Clear byte array.</returns>
        public byte[] Decrypt(ref byte[] byteArray)
        {
            byte[] result = null;
            if (Type == CipherType.TripleDES)
            {
                if (IsTripleCapable())
                {
                    ICryptoTransform transform = null;
                    try
                    {
                        transform = GetTripleDES().CreateDecryptor();
                        result = transform.TransformFinalBlock(byteArray, 0, byteArray.Length);
                    }
                    catch (Exception ex)
                    {
                        throw new PulsarException(typeof(Cipher).FullName, Pulsar.Toolkit.Resources.Exceptions.Cipher.TripleEncryptFailed, ex);
                    }
                    finally
                    {
                        if (transform != null)
                            transform.Dispose();
                    }
                }
                else
                {
                    ThrowTripleParamException();
                }
            }
            else
            {
                throw new PulsarException(typeof(Cipher).FullName, Pulsar.Toolkit.Resources.Exceptions.Cipher.SHA256DecryptFailed);
            }

            return result;
        }

        
        /// <summary>
        /// True if param for a TripleDES operation are valid.
        /// </summary>
        /// <returns>True if the current instance of the Cipher can made TripleDES operation.</returns>
        private bool IsTripleCapable()
        {
            return (this.IV != null && this.IV.Length > 0) && (this.Key != null && this.IV.Length > 0);
        }

        /// <summary>
        /// Throw exception specific to a param problem.
        /// </summary>
        private void ThrowTripleParamException()
        {
            if (this.IV == null || this.IV.Length == 0)
                throw new PulsarException(typeof(Cipher).FullName, Pulsar.Toolkit.Resources.Exceptions.Cipher.TripleIVEmpty);
            if (this.Key == null || this.IV.Length == 0)
                throw new PulsarException(typeof(Cipher).FullName, Pulsar.Toolkit.Resources.Exceptions.Cipher.TripleKeyEmpty);
        }

        /// <summary>
        /// Get the TripleDES cryptographer.
        /// </summary>
        /// <returns>The TripleDES cryptographer.</returns>
        private TripleDES GetTripleDES()
        {
            TripleDES tripleDES = TripleDESCryptoServiceProvider.Create();
            tripleDES.IV = this.IV;
            tripleDES.Key = this.Key;
            return tripleDES;
        }
    }
}
