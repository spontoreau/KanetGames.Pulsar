using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsar.Graphics.Particles.Triggers
{
    public abstract class Trigger
    {
        public abstract int Start(Emitter emitter);
        public abstract int Update(Emitter emitter, float timeElapsed);
    }
}
