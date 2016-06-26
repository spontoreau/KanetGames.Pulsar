using SFML.Graphics;
using System;

namespace Pulsar.Graphics.Particles.Renderers
{
    public sealed class VertexRenderer : Drawable
    {
        private VertexArray _array;
        private Emitter _emitter;

        public VertexRenderer(Emitter emitter)
        {
            this._emitter = emitter;
            this._emitter.Created += new EventHandler(Particles_Created);
            this._emitter.Updated += new EventHandler(Emitter_Updated);
            this._emitter.Empty += new EventHandler(Emitter_Empty);
        }

        private void Particles_Created(object sender, EventArgs args)
        {
            _array = new VertexArray(PrimitiveType.Points, (uint)this._emitter.Particles.Count);
        }
        private void Emitter_Updated(object sender, EventArgs args)
        {
            for(int i = 0, l = this._emitter.Particles.Count; i < l; i++)
            {
                Vertex v = this._array[(uint)i];
                v.Position.X = this._emitter.Particles[i].Position.X;
                v.Position.Y = this._emitter.Particles[i].Position.Y;
                v.Color.A = this._emitter.Particles[i].Color.A;
                v.Color.R = this._emitter.Particles[i].Color.R;
                v.Color.G = this._emitter.Particles[i].Color.G;
                v.Color.B = this._emitter.Particles[i].Color.B;
                this._array[(uint)i] = v;
            }
        }

        private void Emitter_Empty(object sender, EventArgs args)
        {
            this._array.Clear();
        }
    
        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(this._array, states);
        }
    }
}
