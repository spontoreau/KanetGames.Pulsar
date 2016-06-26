using Pulsar.Framework;
using Pulsar.Graphics.Particles.Areas;
using Pulsar.Graphics.Particles.Behaviours;
using Pulsar.Graphics.Particles.Initializers;
using Pulsar.Graphics.Particles.Modifiers;
using Pulsar.Graphics.Particles.Triggers;
using System;
using System.Collections.Generic;

namespace Pulsar.Graphics.Particles
{
    public class Emitter
    {
        /// <summary>
        /// Raise when the trigger of the emitter finished.
        /// </summary>
        public event EventHandler Finished;

        /// <summary>
        /// Raise when the emitter hasn't particle to update
        /// </summary>
        public event EventHandler Empty;

        public event EventHandler Updated;

        public event EventHandler Created;

        /// <summary>
        /// Get or set the list of particle initializers
        /// </summary>
        public List<Initializer> Initializers { get; set; }

        /// <summary>
        /// Get or set the list of particle modifiers
        /// </summary>
        public List<Modifier> Modifiers { get; set; }

        /// <summary>
        /// Get or set the list of emitter behaviours
        /// </summary>
        public List<Behaviour> Behaviours { get; set; }

        /// <summary>
        /// The particle list
        /// </summary>
        public List<Particle> Particles { get; set; }

        /// <summary>
        /// Get or set the Trigger of the emitter
        /// </summary>
        public Trigger Trigger { get; set; }

        /// <summary>
        /// Get or set the area of the emitter
        /// </summary>
        public Area Area { get; set; }

        /// <summary>
        /// True if the emitter is running
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// True if the emitter started
        /// </summary>
        public bool IsStarted { get; private set; }

        /// <summary>
        /// Position of the emitter
        /// </summary>
        public Vector Position { get; set; }

        /// <summary>
        /// Rotation of the emitter in radian
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// Create a new instance of Emitter
        /// </summary>
        public Emitter()
        {
            this.Behaviours = new List<Behaviour>();
            this.Initializers = new List<Initializer>();
            this.IsRunning = false;
            this.IsStarted = false;
            this.Modifiers = new List<Modifier>();
            this.Particles = new List<Particle>();
        }

        /// <summary>
        /// Create a particle and add it to the emitter
        /// </summary>
        public void Create()
        {
            Particle p = new Particle();
            p.SetDefault();

            this.Add(p, false);
        }

        /// <summary>
        /// Add a particle into the emitter.
        /// </summary>
        /// <param name="particle">The particle to add</param>
        /// <param name="isInitialized">True if the particle is initialized and don't need to be intialize with the current emitter initializer list</param>
        public void Add(Particle particle, bool isInitialized = false)
        {
            if (!isInitialized)//if not initialize, do it with the emitter intializers!
            {
                foreach (Initializer i in this.Initializers)
                {
                    i.Initialize(this, particle);
                }
            }

            this.Particles.Add(particle);
        }

        /// <summary>
        /// Start the emitter
        /// </summary>
        public void Start()
        {
            if (!this.IsStarted)
            {
                foreach (Behaviour b in this.Behaviours)//initalize all emitter behaviours
                {
                    b.Initialize(this);
                }

                int amount = this.Trigger.Start(this);//start the trigger

                for (int i = 0; i < amount; i++) //if trigger start with a pop of particles, create them.
                    Create();

                if (amount > 0)
                    OnCreated(EventArgs.Empty);

                this.IsRunning = true;
                this.IsStarted = true;
            }
            else//if emitter is started, simply return and do nothing
            {
                return;
            }
        }

        /// <summary>
        /// Update the emitter and their particles
        /// </summary>
        /// <param name="timeElapsed">Time elasped since last update in milliseconds</param>
        public void Update(float timeElapsed)
        {
            if (this.IsStarted && this.IsRunning)//if emitter started and currently running
            {                
                int amount = this.Trigger.Update(this, timeElapsed);//update the trigger
                for (int i = 0; i < amount; i++)//if trigger update need a pop of particles, create them
                    Create();

                if (amount > 0)
                    OnCreated(EventArgs.Empty);

                foreach (Behaviour b in this.Behaviours)//update emitter behaviours
                    b.Update(this, timeElapsed);

                if (this.Particles.Count > 0)
                {
                    for (int i = 0, l = this.Particles.Count; i < l; i++)//for each particles in the emitter
                    {
                        foreach (Modifier m in this.Modifiers)//apply all modifiers
                        {
                            m.Update(this, this.Particles[i], timeElapsed);
                        }

                        if (!this.Particles[i].IsAlive)//if the particle not alive, remove it.
                        {
                            this.Particles.RemoveAt(i);
                            i--;
                            l--;
                        }
                    }

                    OnUpdated(EventArgs.Empty);
                }
                else
                {
                    //Raise empty event
                    OnEmpty(EventArgs.Empty);
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Pause the emitter
        /// </summary>
        public void Pause()
        {
            this.IsRunning = false;
        }

        /// <summary>
        /// Resume the emitter
        /// </summary>
        public void Resume()
        {
            this.IsRunning = true;
        }

        /// <summary>
        /// Stop the emitter and clear particles
        /// </summary>
        public void Stop()
        {
            this.IsStarted = false;
            this.IsRunning = false;
            this.Particles.Clear();
        }

        /// <summary>
        /// Raise the Empty event
        /// </summary>
        /// <param name="args">Argument of the event</param>
        private void OnEmpty(EventArgs args)
        {
            EventHandler tmp = Empty;

            if (tmp != null)
                Empty(this, args);
        }

        /// <summary>
        /// Raise the Updated event
        /// </summary>
        /// <param name="args">Argument of the event</param>
        private void OnUpdated(EventArgs args)
        {
            ParticleCounter.Amount = this.Particles.Count;
            EventHandler tmp = Updated;

            if (tmp != null)
                Updated(this, args);
        }

        /// <summary>
        /// Raise the Created event
        /// </summary>
        /// <param name="args">Argument of the event</param>
        private void OnCreated(EventArgs args)
        {
            EventHandler tmp = Created;

            if (tmp != null)
                Created(this, args);
        }
    }
}
