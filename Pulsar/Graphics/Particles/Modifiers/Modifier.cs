
namespace Pulsar.Graphics.Particles.Modifiers
{
    /// <summary>
    /// Abstract modifier
    /// </summary>
    public abstract class Modifier
    {
        /// <summary>
        /// Apply the modifier to the particle
        /// </summary>
        /// <param name="emitter">Emitter which call the update</param>
        /// <param name="particle">The particle to update</param>
        /// <param name="timeElasped">Time elasped since last emitter update in milliseconds</param>
        public abstract void Update(Emitter emitter, Particle particle, float timeElasped);
    }
}
