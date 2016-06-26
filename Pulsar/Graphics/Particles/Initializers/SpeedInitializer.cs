using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsar.Graphics.Particles.Initializers
{
    public class SpeedInitializer : Initializer
    {
        private float _speed;

        public float Speed
        {
            get
            {
                return this._speed;
            }
            set
            {
                this._speed = value;
            }
        }

        public SpeedInitializer()
        {

        }

        public override void Initialize(Emitter emitter, Particle particle)
        {
            particle.Velocity = particle.Velocity * this._speed;
        }
    }
}
