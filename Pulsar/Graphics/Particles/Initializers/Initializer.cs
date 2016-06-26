using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsar.Graphics.Particles.Initializers
{
    public abstract class Initializer
    {
        public abstract void Initialize(Emitter emitter, Particle particle);
    }
}
