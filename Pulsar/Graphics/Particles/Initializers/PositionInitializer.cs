using Pulsar.Graphics.Particles.Areas;

namespace Pulsar.Graphics.Particles.Initializers
{
    public sealed class PositionInitializer : Initializer
    {
        public PositionInitializer()
        {

        }

        public override void Initialize(Emitter emitter, Particle particle)
        {
            particle.Position = emitter.Area.DrawPosition() + emitter.Position;
        }
    }
}
