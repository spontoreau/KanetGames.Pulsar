using Pulsar.Framework;

namespace Pulsar.Graphics.Particles.Areas
{
    public abstract class Area
    {
        public abstract bool Contains(Vector v);
        public abstract Vector DrawPosition();
    }
}
