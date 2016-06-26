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

using Pulsar.Director.Events;
using Pulsar.Framework;
using Pulsar.Content;
using SFML.Graphics;
using System;

namespace Pulsar.Director
{
    /// <summary>
    /// Represent a abstract shot in the scene
    /// </summary>
    public abstract class Shot
    {
        /// <summary>
        /// Event raise when transition of the shot start
        /// </summary>
        public event EventHandler TransitionStart;

        /// <summary>
        /// Event raise when transition of the shot stop
        /// </summary>
        public event EventHandler TransitionStop;

        /// <summary>
        /// Event raise when transition position of the shot changed
        /// </summary>
        public event EventHandler<TransitionPositionEventArgs> TransitionPositionChanged;        

        /// <summary>
        /// Position of the transition.
        /// </summary>
        private float _position;

        /// <summary>
        /// 
        /// </summary>
        private ShotStateEnum _state;

        /// <summary>
        /// Position of the transition.
        /// </summary>
        public float TransitionPosition
        {
            get
            {
                return this._position;
            }
            internal set
            {
                if (value != this._position)
                {
                    this._position = value;
                    OnTransitionPositionChanged();
                }
            }
        }

        /// <summary>
        /// True if the shot is a Dialog
        /// </summary>
        public bool IsDialogShot { get; set; }

        /// <summary>
        /// Get or set the dialog background alpha to apply when the shot need to be draw.
        /// </summary>
        public byte DialogBackgroundAlpha { get; set; }

        /// <summary>
        /// View to use with the shot.
        /// </summary>
        public View  View { get; set; }

        /// <summary>
        /// Get or set the time of a transition (on or off)
        /// </summary>
        public TimeSpan TransitionTime { get; set; }

        /// <summary>
        /// Gets the current shot transition state.
        /// </summary>
        public ShotStateEnum State
        {
            get { return this._state; }
            internal set
            {
                if (value != this._state)
                {
                    if (value == ShotStateEnum.TransitionOn || value == ShotStateEnum.TransitionOff)
                        OnTransitionStart();
                    else
                        OnTransitionStop();

                    this._state = value;
                }
            }
        }

        /// <summary>
        /// True if the Shot is leaving from the ShotManager
        /// </summary>
        public bool IsLeaving { get; private set; }

        /// <summary>
        /// Create a new instance of Shot with default Transition time to zero
        /// </summary>
        public Shot()
            : this(TimeSpan.Zero)
        {

        }

        /// <summary>
        /// Create a new instance of Shot
        /// </summary>
        /// <param name="transitionTime">Shot transition time</param>
        public Shot(TimeSpan transitionTime)
        {
            this.TransitionTime = transitionTime;
        }

        /// <summary>
        /// Set the Shot to leaving mode
        /// </summary>
        public void Leave()
        {
            this.IsLeaving = true;
        }        

        /// <summary>
        /// Load graphics content for the shot.
        /// <param name="contentManager">The content manager</param>
        /// </summary>
        public abstract void LoadContent(ContentManager contentManager);

        /// <summary>
        /// Unload content for the shot.
        /// </summary>
        public abstract void UnloadContent();

        /// <summary>
        /// Handle Shot input
        /// </summary>
        public abstract void HandleInput();

        /// <summary>
        /// Draw the Shot
        /// </summary>
        public abstract void Draw(GameTime gameTime);

        /// <summary>
        /// Update the Shot
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Raise the TransitionStart event
        /// </summary>
        private void OnTransitionStart()
        {
            EventHandler tmp = TransitionStart;

            if (tmp != null)
                TransitionStart(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raise the TransitionStop event
        /// </summary>
        protected virtual void OnTransitionStop()
        {
            EventHandler tmp = TransitionStop;

            if (tmp != null)
                TransitionStop(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raise the TransitionPositionChanged event
        /// </summary>
        protected virtual void OnTransitionPositionChanged()
        {
            EventHandler<TransitionPositionEventArgs> tmp = TransitionPositionChanged;

            if (tmp != null)
                TransitionPositionChanged(this, new TransitionPositionEventArgs(this.TransitionPosition));
        }
    }
}
