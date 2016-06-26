using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsar.Graphics.Particles.Triggers
{
    public sealed class Pulse : Trigger
    {
        private float _nextTime;
        private float _pulseInterval;
        private int _amount;
        private bool _isRunning;

        public float Interval
        {
            get
            {
                return this._pulseInterval;
            }
            set
            {
                this._pulseInterval = value;
            }
        }

        public int Amount
        {
            get
            {
                return this._amount;
            }
            set
            {
                this._amount = value;
            }
        }

        public Pulse()
        {

        }

        public override int Start(Emitter emitter)
        {
            this._nextTime = this._pulseInterval;
            return this._amount;
        }

        public override int Update(Emitter emitter, float timeElapsed)
        {
            this._nextTime -= timeElapsed;

            if (this._nextTime < 0.0f)
            {
                this._nextTime = this._pulseInterval;
                return this._amount;
                
            }
            else
            {
                return 0;
            }
        }
    }
}
