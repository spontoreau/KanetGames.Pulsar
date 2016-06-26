using Pulsar.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsar.Graphics.Particles.Modifiers
{
    public sealed class OpacityModifier : Modifier
    {
        private float _min;
        private float _max;

        public float Min
        {
            get
            {
                return this._min;
            }
            set
            {
                this._min = value;
            }
        }

        public float Max
        {
            get
            {
                return this._max;
            }
            set
            {
                this._max = value;
            }
        }

        public override void Update(Emitter emitter, Particle particle, float timeElasped)
        {
            float a = (255f * MathHelper.Clamp(particle.Energy, Min, Max));
            particle.Color.A = (byte)a;
        }
    }
}
