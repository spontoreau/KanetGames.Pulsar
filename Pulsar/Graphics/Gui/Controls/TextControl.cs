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
using Pulsar.Graphics.Gui.Enums;
using SFML.Graphics;
using System;

namespace Pulsar.Graphics.Gui.Controls
{
    /// <summary>
    /// Abstract TextControl contains base method for using text in a control object
    /// </summary>
    public abstract class TextControl : Control, ITextRenderable, ITextFadeCapable, ITextColorCapable
    {
        /// <summary>
        /// Internal class text to use for measure string with font.
        /// </summary>
        private Text _internalText;

        /// <summary>
        /// Real text
        /// </summary>
        private string _text;

        /// <summary>
        /// Text to display when drawing
        /// </summary>
        private string _displayText;

        /// <summary>
        /// Text to show
        /// </summary>
        public virtual string Text
        {
            get
            {
                return this._displayText;
            }
            set
            {
                if (this._text != value)
                {
                    this._text = value;

                    if (this.Font != null)
                    {
                        this._displayText = TextPartToRender();
                        this.TextPosition = GetTextAlignPosition();
                    }
                    OnTextChanged();
                }
            }
        }

        public Vector TextPosition { get; private set; }

        /// <summary>
        /// Text horizontal alignement
        /// </summary>
        public TextHorizontalAlignEnum TextHorizontalAlign { get; set; }

        /// <summary>
        /// Text vertical alignement
        /// </summary>
        public TextVerticalAlignEnum TextVerticalAlign { get; set; }

        /// <summary>
        /// Text font
        /// </summary>
        private Font _font;

        /// <summary>
        /// Text font
        /// </summary>
        public Font Font 
        {
            get
            {
                return this._font;
            }
            set
            {
                this._font = value;
                this._internalText.Font = this._font;
            }
        }

        /// <summary>
        /// Fade to apply on the controle
        /// </summary>
        public float TextFade { get; set; }

        /// <summary>
        /// Control font color
        /// </summary>
        public Color ForeColor { get; set; }

        /// <summary>
        /// Text fade effect
        /// </summary>
        public TextFadeEffect TextFadeEffect { get; set; }

        /// <summary>
        /// Text color effect
        /// </summary>
        public TextColorEffect TextColorEffect { get; set; }

        /// <summary>
        /// TextChanged event
        /// </summary>
        public event EventHandler TextChanged;

        /// <summary>
        /// Raise the TextChanged event
        /// </summary>
        protected internal virtual void OnTextChanged()
        {
            EventHandler tmp = TextChanged;

            if (tmp != null)
                tmp(this, EventArgs.Empty);
        }     

        /// <summary>
        /// Create a new instance of TextControl
        /// </summary>
        public TextControl()
            : base()
        {
            this._internalText = new Text();
        }

        /// <summary>
        /// Update the control
        /// </summary>
        /// <param name="gameTime">Time since last update</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.TextFadeEffect != null)
                this.TextFadeEffect.Apply(gameTime);

            if (this.TextColorEffect != null)
                this.TextColorEffect.Apply(gameTime);
        }

        /// <summary>
        /// Set position of the control
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public override void SetPosition(float x, float y)
        {
            base.SetPosition(x, y);

            if (this.Font != null)
            {
                this._displayText = TextPartToRender();
                this.TextPosition = GetTextAlignPosition();
            }
        }

        /// <summary>
        /// Text part to render in the control
        /// </summary>
        /// <returns>String part to draw</returns>
        protected virtual string TextPartToRender()
        {
            string textPartToShow = string.Empty;

            for (int i = 0, l = this._text.Length; i < l; i++)
            {
                this._internalText.DisplayedString = textPartToShow + this._text[i];
                
                if (this._internalText.GetLocalBounds().Width < this.Bounds.Width)
                {
                    textPartToShow += this._text[i];
                }
                else
                {
                    textPartToShow.Remove(textPartToShow.Length - 4);
                    textPartToShow += "".PadRight(3, '.');
                    break;
                }
            }

            return textPartToShow;
        }

        /// <summary>
        /// Get text align position
        /// </summary>
        /// <returns>Vector position</returns>
        private Vector GetTextAlignPosition()
        {
            this._internalText.DisplayedString = this._displayText;
            FloatRect bounds = this._internalText.GetLocalBounds();
            //Calculer l'alignement
            Vector position = Vector.Zero;

            switch (this.TextHorizontalAlign)
            {
                case TextHorizontalAlignEnum.Right:
                    float xPositionRight = (this.Bounds.Width - bounds.Left) + this.Bounds.X;
                    position.X = xPositionRight;
                    break;
                case TextHorizontalAlignEnum.Center:
                    float xPositionCenter = (((float)this.Bounds.Width - bounds.Left) / 2) + this.Bounds.X;
                    position.X = xPositionCenter;
                    break;
                default:
                    position.X = this.Bounds.X;
                    break;
            }

            switch (this.TextVerticalAlign)
            {
                case TextVerticalAlignEnum.Bottom:
                    float yPositionBottom = ((float)this.Bounds.Height - bounds.Top) + this.Bounds.Y;
                    position.Y = yPositionBottom;
                    break;
                case TextVerticalAlignEnum.Center:
                    float yPositionCenter = (((float)this.Bounds.Height - bounds.Top) / 2) + this.Bounds.Y;
                    position.Y = yPositionCenter;
                    break;
                default:
                    position.Y = this.Bounds.Y;
                    break;
            }

            return position;
        }
     }
}
