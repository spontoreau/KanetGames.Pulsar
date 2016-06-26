using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsar.Graphics.Particles.Triggers
{
    public sealed class Blast : Trigger
    {
        private int _amount;

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

        private bool _isFinished;

        public bool IsFinished
        {
            get
            {
                return this._isFinished;
            }
            set
            {
                this._isFinished = value;
            }
        }

        public Blast()
        {

        }

        public override int Start(Emitter emitter)
        {
            IsFinished = true;
            return this._amount;
        }

        public override int Update(Emitter emitter, float timeElapsed)
        {
            return 0;
        }
    }
}
