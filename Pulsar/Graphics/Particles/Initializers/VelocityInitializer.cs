using Pulsar.Framework;

namespace Pulsar.Graphics.Particles.Initializers
{
    public class VelocityInitializer : Initializer
    {
        private bool _isRandomDraw;

        public bool IsRandomDraw
        {
            get
            {
                return this._isRandomDraw;
            }
            set
            {
                this._isRandomDraw = value;
            }
        }


        public override void Initialize(Emitter emitter, Particle particle)
        {
            Vector position = particle.Position - emitter.Position;
            float angle = MathHelper.Atan2(position.Y, position.X);
            //If it's a random velocity, draw it. Otherwise just get a normalize vector of the particle position for the velocity.
            particle.Velocity = (this._isRandomDraw) ? RandomHelper.NextVector() : new Vector(MathHelper.Cos(angle), MathHelper.Sin(angle));
        }
    }
}
