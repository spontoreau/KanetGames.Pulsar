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

using Gwen.Control;
using Pulsar.Core.Configuration;
using Pulsar.Core.Display;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace Pulsar
{
    public static class GuiContext
    {
        private static Gwen.Input.SFML _input;
        private static Gwen.Renderer.SFML _renderer;
        private static Gwen.Skin.TexturedBase _skin;
        private static GameConfiguration _gameConfiguration;
        private static Resolution _virtualResolution;

        /// <summary>
        /// Raised when a GuiContext created the Canvas.
        /// </summary>
        public static event EventHandler<EventArgs> Created;

        /// <summary>
        /// Raised when a GuiContext creating the Canvas.
        /// </summary>
        public static event EventHandler<EventArgs> Creating;

        /// <summary>
        /// True if gui context was created
        /// </summary>
        public static bool IsCreated { get; private set; }

        /// <summary>
        /// The Canvas to use for game gui
        /// </summary>
        public static Canvas Canvas { get; private set; }

        /// <summary>
        /// Create the GUI Canvas
        /// </summary>
        public static void Initialize(GameConfiguration gameConfiguration, Resolution virtualResolution)
        {
            WindowContext.Created += WindowContext_Created;

            _gameConfiguration = gameConfiguration;
            _virtualResolution = virtualResolution;            
        }

        private static void Create()
        {
            OnGuiContextCreating(EventArgs.Empty);
            //Create the global GwenRenderer and Canvas
            _renderer = new Gwen.Renderer.SFML(WindowContext.Window);
            WindowContext.Window.KeyPressed += KeyPressed;
            WindowContext.Window.KeyReleased += KeyReleased;
            WindowContext.Window.MouseButtonPressed += MouseButtonPressed;
            WindowContext.Window.MouseButtonReleased += MouseButtonReleased;
            WindowContext.Window.MouseWheelMoved += MouseWheelMoved;
            WindowContext.Window.MouseMoved += MouseMoved;
            WindowContext.Window.TextEntered += TextEntered;

            Gwen.Font font = new Gwen.Font(_renderer, _gameConfiguration.SkinFont, 12);
            _renderer.LoadFont(font);

            _skin = new Gwen.Skin.TexturedBase(_renderer, _gameConfiguration.Skin);
            _skin.SetDefaultFont(font.FaceName);
            font.Dispose();

            Canvas canvas = new Canvas(_skin);
            canvas.SetBounds(0, 0, _virtualResolution.Width, _virtualResolution.Height);
            canvas.ShouldDrawBackground = false;
            canvas.KeyboardInputEnabled = true;
            Canvas = canvas;

            _input = new Gwen.Input.SFML();
            _input.Initialize(canvas, WindowContext.Window);

            OnGuiContextCreated(EventArgs.Empty);
        }

        private static void OnGuiContextCreated(EventArgs e)
        {
            EventHandler<EventArgs> tmp = Created;

            if (tmp != null)
                tmp(null, e);
        }

        private static void OnGuiContextCreating(EventArgs e)
        {
            EventHandler<EventArgs> tmp = Creating;

            if (tmp != null)
                tmp(null, e);
        }

        public static void WindowContext_Created(object sender, System.EventArgs e)
        {
            Create();
        }

        public static void Dispose()
        {
            if (Canvas != null)
                Canvas.Dispose();
            if (_skin != null)
                _skin.Dispose();
            if(_renderer != null)
                _renderer.Dispose();            
        }

        private static void TextEntered(object sender, TextEventArgs e)
        {
            _input.ProcessMessage(e);
        }

        private static void MouseMoved(object sender, MouseMoveEventArgs e)
        {
            _input.ProcessMessage(e);
        }

        private static void MouseWheelMoved(object sender, MouseWheelEventArgs e)
        {
            _input.ProcessMessage(e);
        }

        private static void MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            _input.ProcessMessage(new Gwen.Input.SFMLMouseButtonEventArgs(e, true));
        }

        private static void MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            _input.ProcessMessage(new Gwen.Input.SFMLMouseButtonEventArgs(e, false));
        }

        private static void KeyPressed(object sender, KeyEventArgs e)
        {
            _input.ProcessMessage(new Gwen.Input.SFMLKeyEventArgs(e, true));
        }

        private static void KeyReleased(object sender, KeyEventArgs e)
        {
            _input.ProcessMessage(new Gwen.Input.SFMLKeyEventArgs(e, false));
        }

    }
}
