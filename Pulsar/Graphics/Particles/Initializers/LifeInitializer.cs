using Pulsar.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsar.Graphics.Particles.Initializers
{
    public sealed class LifeInitializer : Initializer
    {
        public float Min { get; set; }
        public float Max { get; set; }

        public LifeInitializer()
        {

        }

        public override void Initialize(Emitter emitter, Particle particle)
        {
            particle.Life = (Min == Max) ? Min : RandomHelper.NextFloat(Min, Max);            
        }
    }
}
