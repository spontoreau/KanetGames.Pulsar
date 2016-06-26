﻿/* License
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

namespace Pulsar.Graphics.Gui.Controls
{
    /// <summary>
    /// ImageBox is a Gui component which encapsulate an image.
    /// By default ImageBox are static and don't apply effect for background. user can allow effect appliment by switch the property UseEffects to true.
    /// </summary>
    public class ImageBox : Control
    {
        /// <summary>
        /// Image in the ImageBox
        /// </summary>
        public Texture Image { get; set; }

        /// <summary>
        /// Source rectangle in the texture
        /// </summary>
        public Rectangle SourceRectangle { get; set; }

        /// <summary>
        /// True if image apply effects
        /// </summary>
        public virtual bool UseEffects { get; set; }

        /// <summary>
        /// Create a new instance of a button
        /// </summary>
        public ImageBox()
            : base()
        {

        }

        /// <summary>
        /// Update the control
        /// </summary>
        /// <param name="gameTime">Time since last update</param>
        public override void Update(GameTime gameTime)
        {
            if (UseEffects)
                base.Update(gameTime);
        }
    }
}
