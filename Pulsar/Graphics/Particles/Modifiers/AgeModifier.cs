
namespace Pulsar.Graphics.Particles.Modifiers
{
    /// <summary>
    /// Particle life modifier
    /// </summary>
    public sealed class AgeModifier : Modifier
    {
        /// <summary>
        /// Create a new instance of AgeModifier
        /// </summary>
        public AgeModifier()
        {

        }

        /// <summary>
        /// Apply the modifier to the particle
        /// </summary>
        /// <param name="emitter">Emitter which call the update</param>
        /// <param name="particle">The particle to update</param>
        /// <param name="timeElasped">Time elasped since last emitter update in milliseconds</param>
        public override void Update(Emitter emitter, Particle particle, float timeElasped)
        {
            particle.Age += timeElasped;
        }
    }
}
