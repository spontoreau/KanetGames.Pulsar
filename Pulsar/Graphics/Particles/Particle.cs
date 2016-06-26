using Pulsar.Framework;
using System;

namespace Pulsar.Graphics.Particles
{
    public sealed class Particle : ICloneable
    {
        /// <summary>
        /// Velocity vector
        /// </summary>
        public Vector Velocity { get; set; }

        /// <summary>
        /// Partie life in seconds
        /// </summary>
        public float Life { get; set; }

        /// <summary>
        /// Age of the particle
        /// </summary>
        public float Age { get; set; }

        /// <summary>
        /// Energy in percent of the particle
        /// </summary>
        public float Energy 
        {
            get
            {
                return 1.0f - this.Age / this.Life;
            }
        }

        /// <summary>
        /// Particle current position
        /// </summary>
        public Vector Position { get; set; }

        private Color _color;

        /// <summary>
        /// Particle current color
        /// </summary>
        public Color Color 
        {
            get
            {
                return this._color;
            }
            set
            {
                this._color = value;
            }
        }

        /// <summary>
        /// Particle mass
        /// </summary>
        public float Mass { get; set; }

        /// <summary>
        /// Particle scale
        /// </summary>
        public float Scale { get; set; }

        /// <summary>
        /// True if the particle is alive
        /// </summary>
        public bool IsAlive
        {
            get
            {
                return this.Age < this.Life;
            }
        }

        /// <summary>
        /// Create a new instance of a particle
        /// </summary>
        public Particle()
        {

        }

        /// <summary>
        /// Set default settings to the particle
        /// </summary>
        public void SetDefault()
        {
            this._color = Color.White;
            this.Age = 0.0f;
            this.Life = 0.0f;
            this.Mass = 1.0f;
            this.Position = Vector.Zero;
            this.Velocity = Vector.Zero;
            this.Scale = 1.0f;
            this.Mass = 1.0f;
        }

        /// <summary>
        /// Clone the particle
        /// </summary>
        /// <returns>Object representation of the particle</returns>
        public object Clone()
        {
            Particle clone = new Particle();
            clone.Color = this.Color;
            clone.Age = this.Age;
            clone.Life = this.Life;
            clone.Mass = this.Mass;
            clone.Position = (Vector)this.Position.Clone();
            clone.Scale = this.Scale;
            clone.Velocity = (Vector)this.Velocity.Clone();
            return clone;
        }

        /// <summary>
        /// Clone the particle
        /// </summary>
        /// <returns>Particle clone a the Particle</returns>
        public Particle TypedClone()
        {
            return (Particle)this.Clone();
        }
    }
}
