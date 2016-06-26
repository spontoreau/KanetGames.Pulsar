using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsar.Graphics.Particles.Initializers
{
    public sealed class MassInitializer : Initializer
    {
        public float Mass { get; set; }

        public MassInitializer()
        {

        }

        public override void Initialize(Emitter emitter, Particle particle)
        {
            particle.Mass = this.Mass;
        }
    }
}
