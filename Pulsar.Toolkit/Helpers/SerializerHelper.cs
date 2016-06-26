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
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Pulsar.Toolkit.Helpers
{
    /// <summary>
    /// Helper class for serialize object
    /// </summary>
    public static class SerializerHelper
    {
        /// <summary>
        /// Load a serializable object from a file.
        /// </summary>
        /// <param name="path">Path of the file to load.</param>
        /// <returns>Serialize Object corresponding to the path.</returns>
        public static T Load<T>(string filePath)
        {
            try
            {
                object obj = filePath.EndsWith(Pulsar.Toolkit.Resources.Format.Common.XmlFileExtension) ? 
                        LoadXml(filePath, typeof(T)) :
                        LoadBinary(filePath);

                if(typeof(T) != typeof(object))
                {
                    return (T)Convert.ChangeType(obj, typeof(T));
                }
                else
                {
                    return (T)obj;
                }
            }
            catch (Exception ex)
            {
                throw new PulsarException(typeof(SerializerHelper).FullName, string.Format(Pulsar.Toolkit.Resources.Exceptions.Helper.SerializeObjectLoadFailed, filePath), ex);
            }
        }

        /// <summary>
        /// Load a serializable object from a binary file.
        /// </summary>
        /// <param name="path">Path of the file to load.</param>
        /// <returns>Serialize Object corresponding to the path.</returns>
        private static object LoadBinary(string filePath)
        {
            Stream stream = null;

            try
            {
                //Open stream in open mode.
                stream = File.Open(filePath, FileMode.Open);
                //Binary Formatter instance.
                BinaryFormatter formatter = new BinaryFormatter();
                //Deserialize the storable entity.
                return formatter.Deserialize(stream);
            }
            finally
            {
                //Finally application close the stream.
                if (stream != null)
                    stream.Close();
            }
        }

        /// <summary>
        /// Load a serializable object from a xml file.
        /// </summary>
        /// <param name="path">Path of the file to load.</param>
        /// <returns>Serialize Object corresponding to the path.</returns>
        private static object LoadXml(string filePath, Type type)
        {
            TextReader reader = null;

            try
            {
                XmlSerializer serializer = new XmlSerializer(type);
                reader = new StreamReader(filePath);
                return serializer.Deserialize(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        /// <summary>
        /// Save a serializable object.
        /// </summary>
        /// <param name="filePath">path to save the serializable object.</param>
        /// <param name="obj">Object to serialize</param>
        public static void Save(string filePath, object obj)
        {
            try
            {
                if (filePath.EndsWith(Pulsar.Toolkit.Resources.Format.Common.XmlFileExtension))
                    SaveXml(filePath, obj);
                else
                    SaveBinary(filePath, obj);
            }
            catch (Exception ex)
            {
                throw new PulsarException(typeof(SerializerHelper).FullName, string.Format(Pulsar.Toolkit.Resources.Exceptions.Helper.SerializeObjectSaveFailed, filePath), ex);
            }
        }

        /// <summary>
        /// Save a serializable object into a binary file.
        /// </summary>
        /// <param name="filePath">path to save the serializable object.</param>
        /// <param name="obj">Object to serialize</param>
        private static void SaveBinary(string filePath, object obj)
        {
            Stream stream = null;

            try
            {
                //Open the stream in OpenOrCreate mode
                stream = File.Open(filePath, FileMode.OpenOrCreate);
                //Binary Formatter instance
                BinaryFormatter formatter = new BinaryFormatter();
                //Serialize the storable entity
                formatter.Serialize(stream, obj);
            }
            finally
            {
                //close the stream
                if (stream != null)
                    stream.Close();
            }
        }

        /// <summary>
        /// Save a serializable object into a Xml file.
        /// </summary>
        /// <param name="filePath">path to save the serializable object.</param>
        /// <param name="obj">Object to serialize</param>
        private static void SaveXml(string filePath, object obj)
        {
            TextWriter writer = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                writer = new StreamWriter(filePath);
                serializer.Serialize(writer, obj);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
    }
}
