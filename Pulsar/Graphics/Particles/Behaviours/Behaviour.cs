using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsar.Graphics.Particles.Behaviours
{
    public abstract class Behaviour
    {
        public abstract void Initialize(Emitter e);
        public abstract void Update(Emitter e, float timeElapsed);
    }
}
