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
using System.Text;

namespace Pulsar.Toolkit.Diagnostics
{
    /// <summary>
    /// Logger class. Offer standard logging function.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Space constant.
        /// </summary>
        private const string SPACE = " ";

        /// <summary>
        /// Event raise when a trace is made by the logger.
        /// </summary>
        public static event EventHandler<TraceEventArgs> TraceMade;

        /// <summary>
        /// Synchronize object.
        /// </summary>
        public static object SyncRoot { get; private set; }

        /// <summary>
        /// Path to store the log files.
        /// </summary>
        private static string Folder { get; set; }

        /// <summary>
        /// Application which use the logger.
        /// </summary>
        private static string Application { get; set; }

        /// <summary>
        /// True if LogManager is initialize.
        /// </summary>
        private static bool IsInit { get; set; }

        /// <summary>
        /// Static Ctor.
        /// </summary>
        static Logger()
        {
            SyncRoot = new object();
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
        }

        /// <summary>
        /// Initialize the Logger.
        /// </summary>
        /// <param name="path">Directory to store the log files.</param>
        /// <param name="application">Application which use the logger.</param>
        public static void Initialize(string folder, string application)
        {
            lock (SyncRoot)
            {
                if (!IsInit)
                {
                    if (!Directory.Exists(folder + Path.DirectorySeparatorChar + application))
                    {
                        Directory.CreateDirectory(folder + Path.DirectorySeparatorChar + application);
                    }

                    Folder = folder;
                    Application = application;

                    IsInit = true;
                }
            }
        }

        /// <summary>
        /// Trace a log information.
        /// </summary>
        /// <param name="source">Source of the trace.</param>
        /// <param name="message">Message of the source.</param>
        /// <param name="state">State of the source.</param>
        public static void Trace(string source, string message, TraceState state)
        {
            lock (SyncRoot)
            {
                if (IsInit)
                {
                    StreamWriter sw = null;

                    try
                    {
                        string date = DateTime.Now.ToString(Pulsar.Toolkit.Resources.Format.Logger.Date);
                        StringBuilder sbFileName = new StringBuilder(Folder)
                                                        .Append(Path.DirectorySeparatorChar)
                                                        .Append(Application)
                                                        .Append(Path.DirectorySeparatorChar)
                                                        .Append(date)
                                                        .Append(Pulsar.Toolkit.Resources.Format.Logger.FileExtension);

                        sw = new StreamWriter(sbFileName.ToString(), true, Encoding.UTF8);

                        StringBuilder sbMessage = new StringBuilder(date)
                                                        .Append(SPACE)
                                                        .Append(state.ToString())
                                                        .Append(SPACE)
                                                        .Append(Pulsar.Toolkit.Resources.Format.Logger.StateSeparator)
                                                        .Append(SPACE)
                                                        .Append(source)
                                                        .Append(SPACE)
                                                        .Append(Pulsar.Toolkit.Resources.Format.Logger.SourceSeparator)
                                                        .Append(SPACE)
                                                        .Append(message);

                        sw.WriteLine(sbMessage.ToString());
                        sw.Flush();
                        OnTraceMade(source, message, state);
                    }
                    finally
                    {
                        if (sw != null)
                            sw.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Trace an Exception.
        /// </summary>
        /// <param name="ex">The exception to trace.</param>
        public static void Trace(Exception ex)
        {
            Trace(ex.Source, ex.Message, TraceState.Error);
        }

        /// <summary>
        /// Trace a PulsarException.
        /// </summary>
        /// <param name="ex">The PulsarException to trace.</param>
        public static void Trace(PulsarException ex)
        {
            if (ex.InnerException != null)
                Trace(ex.InnerException);

            Trace(ex.Source, ex.Message, TraceState.Error);//if we don't made this it's throw a stackoverflow ! ';..;'
        }

        /// <summary>
        /// True if the logger is initialize.
        /// </summary>
        /// <returns>True if the logger is initialize.</returns>
        public static bool IsInitialize()
        {
            lock (SyncRoot)
            {
                return IsInit;
            }
        }

        /// <summary>
        /// Raise the TraceMade event.
        /// </summary>
        /// <param name="trace">Trace entity.</param>
        private static void OnTraceMade(string source, string message, TraceState state)
        {
            EventHandler<TraceEventArgs> tmp = TraceMade;

            if(tmp != null)
            {
                Trace trace = new Trace(source, message, state);
                TraceMade(null, new TraceEventArgs(trace));
            }
        }

        /// <summary>
        /// Handle the AppDomain Unhandled Exception.
        /// </summary>
        /// <param name="sender">Sender of the Exception.</param>
        /// <param name="e">Unhandled Exception arguments.</param>
        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try //try catch is here because if we don't catch exception this method will be call again and again and again ! Possible of a stackoverflow here.
            {
                Trace((Exception)e.ExceptionObject);
            }
            catch
            {
                //if we can't Trace the Unhandled Exception, don't do anything !
            }
        }
    }
}
