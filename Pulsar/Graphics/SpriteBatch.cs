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
using SFML.Graphics;
using SFML.Window;

namespace Pulsar.Graphics
{
    /// <summary>
    /// Sprite batcher. Allow to Draw sprites with the same settings. Can perfom multiple draw with saving memory.
    /// </summary>
    public sealed class SpriteBatch : GraphicsBatch
    {
        /// <summary>
        /// Single instance for the sprite batch
        /// </summary>
        private static SpriteBatch _instance;

        /// <summary>
        /// Sprite use to Draw sprite
        /// </summary>
        private Sprite _sprite = new Sprite();

        /// <summary>
        /// Text use to Draw string
        /// </summary>
        private Text _text = new Text();

        /// <summary>
        /// Create a new instance of the SpriteBatch
        /// </summary>
        /// <param name="renderTarget">Render target use by the SpriteBatch</param>
        private SpriteBatch(RenderTarget renderTarget)
            : base(renderTarget)
        {
            this._renderTarget = renderTarget;
        }        

        /// <summary>
        /// Draw a texture
        /// </summary>
        /// <param name="texture">Texture to draw</param>
        /// <param name="destination">Texture destination (position and size)</param>
        /// <param name="source">Source rectangle in the texture to draw</param>
        /// <param name="color">Global color</param>
        /// <param name="rotation">Rotation of the texture</param>
        /// <param name="origin">Origin of the texture</param>
        public void Draw(Texture texture, Rectangle destination, Rectangle source, Color color, float rotation, Vector origin)
        {
            if (this.HasBegin)
            {
                if (source != null)
                    this._sprite.TextureRect = new IntRect((int)source.X,(int)source.Y,(int)source.Width,(int)source.Height);
                else
                {
                    Vector2u v = texture.Size;
                    this._sprite.TextureRect = new IntRect(0, 0, (int)v.X, (int)v.Y);
                }

                this._sprite.Texture = texture;

                Vector2f position = this._sprite.Position;
                position.X = destination.Position.X;
                position.Y = destination.Position.X;

                SFML.Graphics.Color c = this._sprite.Color;
                c.A = color.A;
                c.B = color.B;
                c.G = color.G;
                c.R = color.R;

                this._sprite.Rotation = rotation;

                Vector2f o = this._sprite.Origin;
                o.X = origin.X;
                o.Y = origin.Y;

                this._sprite.Scale = new Vector2f(destination.Width / (float)this._sprite.TextureRect.Width, destination.Height / (float)this._sprite.TextureRect.Height);// TODO extension pour scale
                this._renderTarget.Draw(this._sprite);
            }
            else
            {
                //TODO throw exception
            }
        }

        /// <summary>
        /// Draw a texture
        /// </summary>
        /// <param name="texture">Texture to draw</param>
        /// <param name="destination">Texture destination (position and size)</param>
        /// <param name="source">Source rectangle in the texture to draw</param>
        /// <param name="color">Global color</param>
        public void Draw(Texture texture, Rectangle destination, Rectangle source, Color color)
        {
            Draw(texture, destination, source, color, 0f, Vector.Zero);
        }

        /// <summary>
        /// Draw a texture
        /// </summary>
        /// <param name="texture">Texture to draw</param>
        /// <param name="destination">Texture destination (position and size)</param>
        /// <param name="color">Global color</param>
        public void Draw(Texture texture, Rectangle destination, Color color)
        {
            Draw(texture, destination, null, color);
        }

        /// <summary>
        /// Draw a texture
        /// </summary>
        /// <param name="texture">Texture to draw</param>
        /// <param name="position">Position of the texture</param>
        /// <param name="source">Source rectangle in the texture to draw</param>
        /// <param name="color">Global color</param>
        /// <param name="rotation">Rotation of the texture</param>
        /// <param name="origin">Origin of the texture</param>
        /// <param name="scale">Scale of the texture</param>
        public void Draw(Texture texture, Vector position, Rectangle source, Color color, float rotation, Vector origin, float scale)
        {
            if (this.HasBegin)
            {
                if (source != null)
                    this._sprite.TextureRect = new IntRect((int)source.X, (int)source.Y, (int)source.Width, (int)source.Height);
                else
                {
                    Vector2u v = texture.Size;
                    this._sprite.TextureRect = new IntRect(0, 0, (int)v.X, (int)v.Y);
                }

                this._sprite.Texture = texture;

                Vector2f p = this._sprite.Position;
                p.X = position.X;
                p.Y = position.X;

                SFML.Graphics.Color c = this._sprite.Color;
                c.A = color.A;
                c.B = color.B;
                c.G = color.G;
                c.R = color.R;

                this._sprite.Rotation = MathHelper.ToDegrees(rotation);

                Vector2f o = this._sprite.Origin;
                o.X = origin.X;
                o.Y = origin.Y;

                this._sprite.Scale = new Vector2f(scale, scale);

                this._renderTarget.Draw(this._sprite);
            }
            else
            {
                //TODO throw exception
            }
        }

        /// <summary>
        /// Draw a texture
        /// </summary>
        /// <param name="texture">Texture to draw</param>
        /// <param name="position">Position of the texture</param>
        /// <param name="source">Source rectangle in the texture to draw</param>
        /// <param name="color">Global color</param>
        public void Draw(Texture texture, Vector position, Rectangle source, Color color)
        {
            Draw(texture, position, source, color, 0f, Vector.Zero, 1.0f); 
        }

        /// <summary>
        /// Draw a texture
        /// </summary>
        /// <param name="texture">Texture to draw</param>
        /// <param name="position">Position of the texture</param>
        /// <param name="color">Global color</param>
        public void Draw(Texture texture, Vector position, Color color)
        {
            Draw(texture, position, null, color, 0f, Vector.Zero, 1.0f); 
        }

        /// <summary>
        /// Draw a string
        /// </summary>
        /// <param name="font">Font to use to draw a string</param>
        /// <param name="text">String text to draw</param>
        /// <param name="position">Position of the text</param>
        /// <param name="color">Color of the text</param>
        /// <param name="rotation">Rotation of the text</param>
        /// <param name="origin">Origin of the text</param>
        /// <param name="scale">Scale of the text</param>
        /// <param name="styles">Optional, style of the text</param>
        public void DrawString(Font font, string text, Vector position, Color color, float rotation, Vector origin, float scale, Text.Styles styles = Text.Styles.Regular)
        {
            if (this.HasBegin)
            {
                this._text.Font = font;
                this._text.DisplayedString = text;

                Vector2f p = this._text.Position;
                p.X = position.X;
                p.Y = position.Y;
                this._text.Position = p;

                this._text.Rotation = rotation;
                this._text.Scale = new Vector2f(scale, scale);

                SFML.Graphics.Color c = this._text.Color;
                c.A = color.A;
                c.B = color.B;
                c.G = color.G;
                c.R = color.R;
                this._text.Color = c;

                this._text.Style = styles;
                this._renderTarget.Draw(this._text);
            }
            else
            {
                //TODO throw exception
            }
        }

        /// <summary>
        /// Draw a string
        /// </summary>
        /// <param name="font">Font to use to draw a string</param>
        /// <param name="text">String text to draw</param>
        /// <param name="position">Position of the text</param>
        /// <param name="color">Color of the text</param>
        public void DrawString(Font font, string text, Vector position, Color color)
        {
            DrawString(font, text, position, color, 0.0f, Vector.Zero, 1.0f);
        }

        /// <summary>
        /// Get the game sprite batcher
        /// </summary>
        /// <returns>The sprite batch</returns>
        public static SpriteBatch Get()
        {
            if (_instance == null)
            {
                _instance = new SpriteBatch(WindowContext.Window);
                WindowContext.Created += WindowContext_Created;
            }

            return _instance;
        }

        static void WindowContext_Created(object sender, System.EventArgs e)
        {
            _instance._renderTarget = WindowContext.Window;
        }
    }
}
