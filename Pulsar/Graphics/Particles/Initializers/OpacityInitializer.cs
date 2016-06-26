using Pulsar.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsar.Graphics.Particles.Initializers
{
    public sealed class OpacityInitializer : Initializer
    {
        public float Min { get; set; }
        public float Max { get; set; }

        public OpacityInitializer()
        {

        }

        public override void Initialize(Emitter emitter, Particle particle)
        {
            particle.Color.A = (byte)(((Min == Max) ? Min : RandomHelper.NextFloat(Min, Max)) * 255);
        }
    }
}
