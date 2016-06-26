using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsar.Graphics.Particles.Modifiers
{
    public sealed class PositionModifier : Modifier
    {
        public override void Update(Emitter emitter, Particle particle, float timeElasped)
        {
            particle.Position += (particle.Velocity * timeElasped);
        }
    }
}
