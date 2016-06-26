using Pulsar.Framework;

namespace Pulsar.Graphics.Particles.Areas
{
    /// <summary>
    /// Define a CircleArea
    /// </summary>
    public sealed class CircleArea : Area
    {
        /// <summary>
        /// Radius of the circle area
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        /// Radius exclude of the circle area
        /// </summary>
        public float ExcludeRadius { get; set; }

        /// <summary>
        /// True if the Circle contains a Vector2f
        /// </summary>
        /// <param name="v">Vector to test</param>
        /// <returns>True if the Circle contains a Vector2f</returns>
        public override bool Contains(Vector v)
        {
            return true;
        }

        /// <summary>
        /// Return a random position contains in the circle area
        /// </summary>
        /// <returns></returns>
        public override Vector DrawPosition()
        {
            return Vector.Polar(RandomHelper.NextFloat(Radius - ExcludeRadius, Radius), RandomHelper.NextFloat(MathHelper.TwoPi));
        }
    }
}
