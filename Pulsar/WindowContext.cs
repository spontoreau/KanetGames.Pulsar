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

using SFML.Graphics;
using SFML.Window;
using System;
using System.Reflection;

namespace Pulsar
{
    /// <summary>
    /// The Window context
    /// </summary>
    public static class WindowContext
    {
        /// <summary>
        /// Default Window VideoMode
        /// </summary>
        private static VideoMode DEFAULT_WINDOW_VIDEO_MODE = new VideoMode(800, 600, 32);

        /// <summary>
        /// Default Window Title
        /// </summary>
        private static string DEFAULT_WINDOW_TITLE = Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>
        /// Default Window Styles
        /// </summary>
        private const Styles DEFAULT_WINDOW_STYLE = Styles.Default;

        /// <summary>
        /// Raised when a WindowContext start to create the RenderWindow.
        /// </summary>
        public static event EventHandler<EventArgs> Creating;

        /// <summary>
        /// True if window was created
        /// </summary>
        public static bool IsCreated { get; private set; }

        /// <summary>
        /// Raised when a WindowContext created the RenderWindow.
        /// </summary>
        public static event EventHandler<EventArgs> Created;

        /// <summary>
        /// The RenderWindow to use in the game context
        /// </summary>
        public static RenderWindow Window { get; private set; }

        /// <summary>
        /// Default Create method
        /// </summary>
        internal static void Create()
        {
            Create(DEFAULT_WINDOW_VIDEO_MODE, DEFAULT_WINDOW_TITLE, DEFAULT_WINDOW_STYLE);
        }

        /// <summary>
        /// Create the RenderWindow
        /// </summary>
        /// <param name="videoMode">VideoMode to apply</param>
        /// <param name="title">Title of the RenderWindow</param>
        /// <param name="styles">Styles of the RenderWindow</param>
        public static void Create(VideoMode videoMode, string title, Styles styles)
        {
            IsCreated = false;
            OnCreating(EventArgs.Empty);
            View view = null;
            if (Window != null)
            {
                view = Window.GetView();
                Window.Close();
                //Window.Dispose();
            }
            Window = new RenderWindow(videoMode, title, styles);

            if(view != null)
             Window.SetView(view);

            Window.SetActive(true);

            IsCreated = true;
            OnCreated(EventArgs.Empty);
        }

        /// <summary>
        /// Called when the WindowContext start to create the RenderWindow. Raises the Creating event.
        /// </summary>
        /// <param name="eventArgs">Arguments to the Creating event.</param>
        private static void OnCreating(EventArgs eventArgs)
        {
            EventHandler<EventArgs> tmp = Creating;

            if (tmp != null)
                Creating(null, eventArgs);
        }

        /// <summary>
        /// Called when the WindowContext created the RenderWindow. Raises the Create event.
        /// </summary>
        /// <param name="eventArgs">Arguments to the Create event.</param>
        private static void OnCreated(EventArgs eventArgs)
        {
            EventHandler<EventArgs> tmp = Created;

            if (tmp != null)
                Created(null, eventArgs);
        }
    }
}
