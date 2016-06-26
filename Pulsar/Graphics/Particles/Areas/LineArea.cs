using Pulsar.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsar.Graphics.Particles.Areas
{
    public class LineArea : Area
    {
        private float _length;
        private float _angle;
        private bool _bothWays;
        private bool _rectilinear;
        
        public float Lenght
        {
            get
            {
                return this._length;
            }
            set
            {
                this._length = value;
            }
        }

        public float Angle
        {
            get
            {
                return this._angle;
            }
            set
            {
                this._angle = value;
            }
        }

        public bool BothWays
        {
            get
            {
                return this._bothWays;
            }
            set
            {
                this._bothWays = value;
            }
        }

        public bool Rectilinear
        {
            get
            {
                return this._rectilinear;
            }
            set
            {
                this._rectilinear = value;
            }
        }

        public LineArea()
        {

        }

        public override bool Contains(Vector v)
        {
            throw new NotImplementedException();
        }

        public override Vector DrawPosition()
        {
            return Vector.Polar(RandomHelper.NextFloat((this._bothWays) ? -this._length : 0.0f, this._length), this._angle);
        }
    }
}
