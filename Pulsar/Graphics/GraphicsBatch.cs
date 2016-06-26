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

using Pulsar.Core.Display;
using SFML.Graphics;
using SFML.Window;

namespace Pulsar.Graphics
{
    /// <summary>
    /// Abstract batcher dedicate to draw Graphics elements
    /// </summary>
    public abstract class GraphicsBatch
    {
        /// <summary>
        /// View use to Draw
        /// </summary>
        private View _view = new View();

        /// <summary>
        /// The render target
        /// </summary>
        protected RenderTarget _renderTarget;

        /// <summary>
        /// The state to apply to the RenderTarget
        /// </summary>
        protected RenderStates _states = RenderStates.Default;

        /// <summary>
        /// True if GraphicsBatch has begin
        /// </summary>
        public bool HasBegin { get; private set; }

        /// <summary>
        /// Create a new instance of the GraphicsBatch
        /// </summary>
        /// <param name="renderTarget">Render target use by the GraphicsBatch</param>
        protected GraphicsBatch(RenderTarget renderTarget)
        {
            this._renderTarget = renderTarget;
            this._view = renderTarget.GetView();
        }

        /// <summary>
        /// Start the GraphicsBatch and apply settings to the current view
        /// </summary>
        /// <param name="blendMode">BlendMode to apply</param>
        /// <param name="bounds">Float rectangle bounds</param>
        /// <param name="size">Size of the view</param>
        /// <param name="rotation">Rotation of the view</param>
        public void Begin(BlendMode blendMode, FloatRect bounds, Vector2f center, Vector2f size, float rotation)
        {
            this._states.BlendMode = blendMode;
            this._view.Reset(bounds);
            this._view.Center = center;
            this._view.Size = size;
            this._view.Rotate(rotation);
            this._renderTarget.SetView(this._view);
            this.HasBegin = true;
        }

        /// <summary>
        /// Start the GraphicsBatch and apply settings of a specific view
        /// </summary>
        /// <param name="blendMode">BlendMode</param>
        /// <param name="view">View with settings to apply</param>
        public void Begin(BlendMode blendMode, View view)
        {
            Begin(blendMode, view.Viewport, view.Center, view.Size, view.Rotation);
        }

        /// <summary>
        /// Start the GraphicsBatch with default view settings
        /// </summary>
        /// <param name="blendMode">BlendMode</param>
        public void Begin(BlendMode blendMode)
        {
            Begin(blendMode, this._renderTarget.GetView());
        }

        /// <summary>
        /// Start the GraphicsBatch with default settings
        /// </summary>
        public void Begin()
        {
            Begin(BlendMode.Alpha);
        }

        /// <summary>
        /// End the GraphicsBatch
        /// </summary>
        public void End()
        {
            this.HasBegin = false;
        }
    }
}
