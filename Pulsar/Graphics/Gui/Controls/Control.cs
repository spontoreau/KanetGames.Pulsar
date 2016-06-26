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

using Pulsar.Framework;
using Pulsar.Graphics.Gui.Effects;
using Pulsar.Graphics.Gui.Effects.Interfaces;
using SFML.Window;
using System;

namespace Pulsar.Graphics.Gui.Controls
{
    /// <summary>
    /// Abstract GUI Control.
    /// All control are capable to deal fade effect and color effect.
    /// </summary>
    public abstract class Control : IFadeCapable, IColorCapable
    {
        /// <summary>
        /// Control bounds
        /// </summary>
        public Rectangle Bounds { get; private set; }

        /// <summary>
        /// True if control is Visible
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// True if control is Enable
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// True if control has focus
        /// </summary>
        public bool HasFocus { get; private set; }

        /// <summary>
        /// Control fade effect
        /// </summary>
        public FadeEffect FadeEffect { get; set; }

        /// <summary>
        /// Control color effect
        /// </summary>
        public ColorEffect ColorEffect { get; set; }

        /// <summary>
        /// Control fade
        /// </summary>
        public float Fade { get; set; }

        /// <summary>
        /// True if control need to fade in
        /// </summary>
        public bool FadeIn
        {
            get 
            {
                return this._isMouseOver;//only if mouse is over the control
            }
        }

        /// <summary>
        /// Control background
        /// </summary>
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// True if control background need to be color in
        /// </summary>
        public bool ColorIn
        {
            get 
            {
                return this._isMouseOver;//only if mouse is over the control
            }
        }

        /// <summary>
        /// True if mouse is over the control
        /// </summary>
        internal bool _isMouseOver;

        /// <summary>
        /// Event raise when the Control is click
        /// </summary>
        public event EventHandler Click;

        /// <summary>
        /// Event raise when mouse enter in control bounds 
        /// </summary>
        public event EventHandler Enter;

        /// <summary>
        /// Event raise when mouse leave the control bounds
        /// </summary>
        public event EventHandler Leave;

        /// <summary>
        /// Event raise if the mouse is over the control
        /// </summary>
        public event EventHandler Over;

        /// <summary>
        /// Raise the click event
        /// </summary>
        /// <param name="args">MouseButton event arguments</param>
        protected internal virtual void OnClick(MouseButtonEventArgs args)
        {
            EventHandler tmp = Click;

            if (tmp != null)
                tmp(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raise the enter event.
        /// </summary>
        protected internal virtual void OnEnter()
        {
            this._isMouseOver = true;

            EventHandler tmp = Enter;

            if (tmp != null)
                tmp(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raise the leave event.
        /// </summary>
        protected internal virtual void OnLeave()
        {
            this._isMouseOver = false;

            EventHandler tmp = Leave;

            if (tmp != null)
                tmp(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raise the over event.
        /// </summary>
        protected internal virtual void OnOver()
        {
            EventHandler tmp = Over;

            if (tmp != null)
                tmp(this, EventArgs.Empty);
        }

        /// <summary>
        /// Create a new instance of Control
        /// </summary>
        public Control()
        {
            this.Visible = true;
            this.Enable = true;
            this.Bounds = Rectangle.Empty;
        }

        /// <summary>
        /// Update the control
        /// </summary>
        /// <param name="gameTime">Time since last update</param>
        public virtual void Update(GameTime gameTime)
        {
            if (this.FadeEffect != null)
                FadeEffect.Apply(gameTime);

            if (this.ColorEffect != null)
                ColorEffect.Apply(gameTime);
        }

        /// <summary>
        /// Set size of the control
        /// </summary>
        /// <param name="width">Width of the control</param>
        /// <param name="height">Height of the control</param>
        public virtual void SetSize(float width, float height)
        {
            this.Bounds.Width = width;
            this.Bounds.Height = height;
        }

        /// <summary>
        /// Set size of the control
        /// </summary>
        /// <param name="v">Vector representing the size</param>
        public void SetSize(Vector v)
        {
            SetSize(v.X, v.Y);
        }

        /// <summary>
        /// Set position of the control
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public virtual void SetPosition(float x, float y)
        {
            this.Bounds.X = x;
            this.Bounds.Y = y;
        }

        /// <summary>
        /// Set position of the control
        /// </summary>
        /// <param name="position">Vector position</param>
        public void SetPosition(Vector position)
        {
            SetPosition(position.X, position.Y);
        }

        /// <summary>
        /// Set controls bounds
        /// </summary>
        /// <param name="bounds">Rectangle corresponding to the control bounds</param>
        public void SetBounds(Rectangle bounds)
        {
            SetSize(bounds.Width, bounds.Height);
            SetPosition(bounds.X, bounds.Y);
        }

        /// <summary>
        /// Focus the control
        /// </summary>
        public void Focus()
        {
            if (!HasFocus)
            {
                HasFocus = true;
                GuiManager.SetFocusTo(this);
            }
        }
    }
}
