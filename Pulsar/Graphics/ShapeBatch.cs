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

namespace Pulsar.Graphics
{
    /// <summary>
    /// Shape batcher. Allow to Draw shape with the same settings. Can perfom multiple draw with saving memory.
    /// </summary>
    public sealed class ShapeBatch : GraphicsBatch
    {
        /// <summary>
        /// Single instance for the shape batch
        /// </summary>
        private static ShapeBatch _instance;

        /// <summary>
        /// Rectangle Shape use to Draw rectangle
        /// </summary>
        private RectangleShape _rect = new RectangleShape();

        /// <summary>
        /// Rectangle Shape use to Draw rectangle
        /// </summary>
        private CircleShape _circle = new CircleShape();

        /// <summary>
        /// Create a new instance of the ShapeBatch
        /// </summary>
        /// <param name="renderTarget">Render target use by the ShapeBatch</param>
        private ShapeBatch(RenderTarget renderTarget)
            : base(renderTarget)
        {
            this._renderTarget = renderTarget;
        }

        /// <summary>
        /// Get the game shape batcher
        /// </summary>
        /// <returns>The shape batch</returns>
        public static ShapeBatch Get()
        {
            if (_instance == null)
                _instance = new ShapeBatch(WindowContext.Window);

            return _instance;
        }
    }
}
