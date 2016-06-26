using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsar.Graphics.Particles.Modifiers
{
    /// <summary>
    /// Particle Mass modifier. Update mass reference to the current particle Energy
    /// </summary>
    public sealed class MassModifier : Modifier
    {
        /// <summary>
        /// Mass end
        /// </summary>
        private float End { get; set; }

        /// <summary>
        /// Mass start
        /// </summary>
        public float Start { get; set; }

        /// <summary>
        /// Create a new instance of Mass modifier
        /// </summary>
        public MassModifier()
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
            //particle.Mass = ;
        }
    }
}
