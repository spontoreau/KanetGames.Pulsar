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

using Pulsar.Content;
using Pulsar.Framework;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pulsar
{
    /// <summary>
    /// Provides basic graphics device initialization, game logic, and rendering code. 
    /// </summary>
    public class RenderableGame : Game
    {
        /// <summary>
        /// Indicates whether the game is currently the active application.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating if the game stop when the application is unactive
        /// </summary>
        public bool IsInactiveStopThread { get; set; }

        /// <summary>
        /// IsFixedTimeStep attribute. By Default it's set to true (VSync active).
        /// </summary>
        private bool _isFixedTimeStep;

        /// <summary>
        /// Gets or sets a value indicating whether to use fixed time steps (VSync 60 FPS if true).
        /// </summary>
        public bool IsFixedTimeStep
        {
            get
            {
                return this._isFixedTimeStep;
            }
            set
            {
                if (WindowContext.IsCreated)
                {
                    this._isFixedTimeStep = value;
                    WindowContext.Window.SetVerticalSyncEnabled(false);
                    WindowContext.Window.SetFramerateLimit((value) ? 120U : 0U);//uint force to multiple by 2 to obtain refresh at 60fps
                }
            }
        }

        /// <summary>
        /// IsMouseVisible attribute. By Default it's set to false.
        /// </summary>
        private bool _isMouseVisible;

        /// <summary>
        /// Gets or sets a value indicating whether the mouse cursor should be visible. 
        /// </summary>
        public bool IsMouseVisible
        {
            get
            {
                return this.IsMouseVisible;
            }
            set
            {
                if (WindowContext.IsCreated)
                {
                    this._isMouseVisible = value;
                    WindowContext.Window.SetMouseCursorVisible(value);
                }
            }
        }

        /// <summary>
        /// Get the content manager.
        /// </summary>
        public ContentManager Content { get; private set; }

        /// <summary>
        /// Get or define the collection of IDrawable which need to be draw.
        /// </summary>
        private Collection<IDrawable> DrawableComponents { get; set; }

        /// <summary>
        /// Initializes a new instance of this class, which provides basic graphics device initialization, game logic, rendering code, and a game loop.
        /// </summary>
        public RenderableGame()
            : base()
        {
            DrawableComponents = new Collection<IDrawable>();
            Content = new ContentManager();
        }

        /// <summary>
        /// Updates the game's clock and calls Update and Draw.
        /// </summary>
        protected override void Tick()
        {
            base.Tick();

            BeginDraw();
            Draw(this.GameTime);
            EndDraw();
        }

        protected override void BeginUpdate()
        {
            base.BeginUpdate();
            if (WindowContext.IsCreated)
                WindowContext.Window.DispatchEvents();
        }

        protected override void EndUpdate()
        {
            
        }

        /// <summary>
        /// Called when the game determines it is time to draw a frame.
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Draw.</param>
        protected virtual void Draw(GameTime gameTime)
        {
            foreach (IDrawable drawable in this.DrawableComponents)
            {
                if (drawable.Visible)
                    drawable.Draw(gameTime);
            }
        }

        /// <summary>
        /// Starts the drawing of a frame. This method is followed by calls to Draw and EndDraw. 
        /// </summary>
        protected virtual void BeginDraw()
        {
            if (WindowContext.IsCreated)
                WindowContext.Window.Clear();
        }

        /// <summary>
        /// Ends the drawing of a frame. This method is preceeded by calls to Draw and BeginDraw. 
        /// </summary>
        protected virtual void EndDraw()
        {
            if (WindowContext.IsCreated)
                WindowContext.Window.Display();

            //Update the timer
            UpdateGameTime();
        }

        protected override void BeginInitialize()
        {
            base.BeginInitialize();
            WindowContext.Created += new EventHandler<EventArgs>(RenderWindowCreated);
            WindowContext.Creating += new EventHandler<EventArgs>(RenderWindowCreating);
        }

        /// <summary>
        /// End the initialize of the game. This method is preceeded calls to Initialize and BeginInitialize. 
        /// </summary>
        protected override void EndInitialize()
        {
            if (!WindowContext.IsCreated)
                WindowContext.Create();
        }

        protected override void UnloadContent()
        {
            GuiContext.Dispose();//alwways need to dispose Gwen component before exiting
            base.UnloadContent();
        }

        /// <summary>
        /// Handle the ComponentAdded event raise by the GameComponentCollection
        /// </summary>
        /// <param name="sender">The GameComponent collection</param>
        /// <param name="eventArgs">Arguments of the ComponentAdded event.</param>
        protected override void GameComponentAdded(object sender, GameComponentCollectionEventArgs eventArgs)
        {
            base.GameComponentAdded(sender, eventArgs);

            if (eventArgs.Component is IDrawable)
            {
                IDrawable drawable = (IDrawable)eventArgs.Component;
                InsertDrawable(drawable);
                drawable.DrawOrderChanged += new EventHandler<EventArgs>(DrawOrderChanged);
            }
        }

        /// <summary>
        /// Handle the ComponentRemoved event raise by the GameComponentCollection
        /// </summary>
        /// <param name="sender">The GameComponent collection</param>
        /// <param name="eventArgs">Arguments of the ComponentRemoved event.</param>
        protected override void GameComponentRemoved(object sender, GameComponentCollectionEventArgs eventArgs)
        {
            base.GameComponentRemoved(sender, eventArgs);
            
            if (eventArgs.Component is IDrawable)
            {
                IDrawable drawable = (IDrawable)eventArgs.Component;
                this.DrawableComponents.Remove(drawable);
                drawable.DrawOrderChanged -= new EventHandler<EventArgs>(DrawOrderChanged);
            }
        }

        /// <summary>
        /// Handle the DrawOrderChanged raise by a IDrawable
        /// </summary>
        /// <param name="sender">The IDrawable</param>
        /// <param name="eventArgs">Arguments of the DrawOrderChanged event.</param>
        private void DrawOrderChanged(object sender, EventArgs eventArgs)
        {
            IDrawable drawable = (IDrawable)sender;
            this.DrawableComponents.Remove(drawable);
            InsertDrawable(drawable);
        }

        /// <summary>
        /// Insert drawable at the right index in the DrawableComponents List.
        /// </summary>
        /// <param name="drawable">The drawable component to insert</param>
        private void InsertDrawable(IDrawable drawable)
        {
            //find the follower and insert the drawable at the index of the follower
            IDrawable follower = (from u in DrawableComponents where u.DrawOrder >= drawable.DrawOrder select u).FirstOrDefault();
            //if follower is null, drawable is the last in the collection, otherwise take the index of the follower
            int index = (follower == null) ? this.DrawableComponents.Count : this.DrawableComponents.IndexOf(follower);
            this.DrawableComponents.Insert(index, drawable);
        }

        /// <summary>
        /// Handle the GainedFocus event raise by the RenderWindow.
        /// </summary>
        /// <param name="sender">The RenderWindow</param>
        /// <param name="eventArgs">Arguments of the GainedFocus event.</param>
        protected virtual void GainedFocus(object sender, EventArgs eventArgs)
        {
            this.IsActive = true;
        }

        /// <summary>
        /// Handle the LostFocus event raise by the RenderWindow.
        /// </summary>
        /// <param name="sender">The RenderWindow</param>
        /// <param name="eventArgs">Arguments of the LostFocus event.</param>
        protected virtual void LostFocus(object sender, EventArgs eventArgs)
        {
            this.IsActive = false;
        }

        /// <summary>
        /// Handle the Closed event raise by the RenderWindow.
        /// </summary>
        /// <param name="sender">The RenderWindow</param>
        /// <param name="eventArgs">Arguments of the LostFocus event.</param>
        protected virtual void Closed(object sender, EventArgs eventArgs)
        {
            Exit();
        }

        /// <summary>
        /// Handle the Creating event raise by the RenderWindowManager.
        /// </summary>
        /// <param name="sender">The RenderWindowManager</param>
        /// <param name="eventArgs">Arguments of the Creating event.</param>
        protected virtual void RenderWindowCreating(object sender, EventArgs eventArgs)
        {
            this.IsActive = false;
        }

        /// <summary>
        /// Handle the Created event raise by the RenderWindowManager.
        /// </summary>
        /// <param name="sender">The RenderWindowManager</param>
        /// <param name="eventArgs">Arguments of the Created event.</param>
        protected virtual void RenderWindowCreated(object sender, EventArgs eventArgs)
        {
            WindowContext.Window.GainedFocus += new EventHandler(GainedFocus);
            WindowContext.Window.LostFocus += new EventHandler(LostFocus);
            WindowContext.Window.Closed += new EventHandler(Closed);
            this.IsActive = true;
        }
    }
}
