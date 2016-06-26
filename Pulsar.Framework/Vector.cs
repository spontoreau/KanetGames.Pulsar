/* License
 * 
 * The MIT License (MIT)
 *
 * Copyright (c) 2013, Kanet Games (contact@kanetgames.com / www.kanetgames.com)
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;

namespace Pulsar.Framework
{
    /// <summary>
    /// Vector with 2 coordinates
    /// </summary>
    [Serializable()]
    public sealed class Vector : ICloneable, IEquatable<Vector>
    {
        private float _x;
        private float _y;

        /// <summary>
        /// Get a x=0f and y=0f vector
        /// </summary>
        public static Vector Zero
        {
            get
            {
                return new Vector(0.0f, 0.0f);
            }
        }

        /// <summary>
        /// Get a x=1f and y=1f vector
        /// </summary>
        public static Vector One
        {
            get
            {
                return new Vector(1.0f, 1.0f);
            }
        }

        /// <summary>
        /// X value
        /// </summary>
        public float X
        {
            get
            {
                return this._x;
            }
            set
            {
                this._x = value;
            }
        }

        /// <summary>
        /// Y value
        /// </summary>
        public float Y
        {
            get
            {
                return this._y;
            }
            set
            {
                this._y = value;
            }
        }

        /// <summary>
        /// Create a new instance of a Vector
        /// </summary>
        /// <param name="x">Float value corresponding to x</param>
        /// <param name="y">Float value corresponding to y</param>
        public Vector(float x, float y)
        {
            this._x = x;
            this._y = y;
        }

        /// <summary>
        /// Create a new instance of a Vector
        /// </summary>
        /// <param name="value">Float value corresponding to x and y</param>
        public Vector(float value)
        {
            this._x = value;
            this._y = value;
        }

        /// <summary>
        /// Negate a vector
        /// </summary>
        /// <param name="v">Vector</param>
        /// <returns>Negated vector</returns>
        public static Vector operator -(Vector v)
        {
            return new Vector(-v._x, -v._x);
        }
        
        /// <summary>
        /// Substract a vector to an other vector
        /// </summary>
        /// <param name="v1">Vector 1</param>
        /// <param name="v2">Vector 2</param>
        /// <returns>Substracred vector</returns>
        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1._x - v2._x, v1._y - v2._y);
        }

        /// <summary>
        /// Add a vector to an other vector
        /// </summary>
        /// <param name="v1">Vector 1</param>
        /// <param name="v2">Vector 2</param>
        /// <returns>Added vector</returns>
        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1._x + v2._x, v1._y + v2._y);
        }

        /// <summary>
        /// Mutiple a vector by a value
        /// </summary>
        /// <param name="v">Vector to multiple</param>
        /// <param name="value">Value</param>
        /// <returns>Multipled vector</returns>
        public static Vector operator *(Vector v, float value)
        {
            return new Vector(v._x * value, v._y * value);
        }

        /// <summary>
        /// Multiple of two vector
        /// </summary>
        /// <param name="v1">Vector 1</param>
        /// <param name="v2">Vector 2</param>
        /// <returns>Multipled vector</returns>
        public static Vector operator *(Vector v1, Vector v2)
        {
            return new Vector(v1._x * v2._x, v1._y * v2._y);
        }

        /// <summary>
        /// Divide a vector by a vector
        /// </summary>
        /// <param name="v1">Vector to divide</param>
        /// <param name="v2">Vector</param>
        /// <returns>Divide vector</returns>
        public static Vector operator /(Vector v1, Vector v2)
        {
            return new Vector(v1._x / v2._x, v1._y / v2._y);
        }

        /// <summary>
        /// Divide a vector by a value
        /// </summary>
        /// <param name="v">Vector to divide</param>
        /// <param name="value">Value</param>
        /// <returns>Divided vector</returns>
        public static Vector operator /(Vector v, float value)
        {
            return new Vector(v._x / value, v._y / value);
        }

        /// <summary>
        /// True if Vector 2 is equal to Vector 1
        /// </summary>
        /// <param name="v1">Vector 1</param>
        /// <param name="v2">Vector 2</param>
        /// <returns>True if Vector 2 is equal to Vector 1</returns>
        public static bool operator ==(Vector v1, Vector v2)
        {
            return v1.Equals(v2);
        }

        /// <summary>
        /// True if Vector 2 isn't equal to Vector 1
        /// </summary>
        /// <param name="v1">Vector 1</param>
        /// <param name="v2">Vector 2</param>
        /// <returns>True if Vector 2 isn't equal to Vector 1</returns>
        public static bool operator !=(Vector v1, Vector v2)
        {
            return !v1.Equals(v2);
        }

        /// <summary>
        /// Force a vector to be within a Min and a Max value
        /// </summary>
        /// <param name="v1">The vector</param>
        /// <param name="vMin">Min vector value</param>
        /// <param name="vMax">Max vector value</param>
        /// <returns>The clamp vector</returns>
        public static Vector Clamp(Vector v1, Vector vMin, Vector vMax)
        {
            return new Vector(MathHelper.Clamp(v1._x, vMin._x, vMax._x), MathHelper.Clamp(v1._y, vMin._y, vMax._y));
        }

        /// <summary>
        /// Get the distance between Vector 1 and 2
        /// </summary>
        /// <param name="v1">Vector 1</param>
        /// <param name="v2">Vector 2</param>
        /// <returns>The distance between Vector 1 and 2</returns>
        public static float Distance(Vector v1, Vector v2)
        {
            return MathHelper.Sqrt(MathHelper.Square(v1._x - v2._x) + MathHelper.Square(v1._y - v2._y));
        }

        /// <summary>
        /// Get the length of the Vector
        /// </summary>
        /// <returns>The length of the Vector</returns>
        public float Length()
        {
            return MathHelper.Sqrt( MathHelper.Square(this._x) + MathHelper.Square(this._y));
        }

        /// <summary>
        /// Linear interpolation between two vector. Passing amount a value of 0 will cause vector 1 to be returned, a value of 1 will cause vector 2 to be returned.
        /// </summary>
        /// <param name="v1">Source value</param>
        /// <param name="v2">Reach value</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of vector 2.</param>
        /// <returns>Vector corresponding to the Lerp</returns>
        public static Vector Lerp(Vector v1, Vector v2, float amount)
        {
            return new Vector(MathHelper.Lerp(v1._x, v2._x, amount), MathHelper.Lerp(v1._y, v2._x, amount));
        }

        /// <summary>
        /// Round the vector
        /// </summary>
        public void Round()
        {
            this._x = MathHelper.Round(this._x);
            this._y = MathHelper.Round(this._y);
        }

        /// <summary>
        /// Normalize the vector
        /// </summary>
        /// <param name="v">A vector to normalize</param>
        /// <returns>Normalize representation of the vector</returns>
        public void Normalize()
        {
            float value = 1.0f / MathHelper.Sqrt(MathHelper.Square(this._x) + MathHelper.Square(this._y));
            this._x = this._x * value;
            this._y = this._y * value;
        }

        /// <summary>
        /// Normalize a vector
        /// </summary>
        /// <param name="v">A vector to normalize</param>
        /// <returns>Normalize representation of the vector</returns>
        public static Vector Normalize(Vector v)
        {
            float value = 1.0f / MathHelper.Sqrt(MathHelper.Square(v._x) + MathHelper.Square(v._y));
            return new Vector(v._x * value, v._y * value);
        }

        /// <summary>
        /// Convert polair coordinates into a Cartesian Vector
        /// </summary>
        /// <param name="lenght">Length coordinate</param>
        /// <param name="angle">Angle in radian</param>
        /// <returns>The vector</returns>
        public static Vector Polar(float lenght, float angle)
        {
            return new Vector(lenght * MathHelper.Cos(angle), lenght * MathHelper.Sin(angle));
        }

        /// <summary>
        /// Clone the vector
        /// </summary>
        /// <returns>Cloned vector</returns>
        public object Clone()
        {
            return new Vector(this._x, this._y);
        }

        /// <summary>
        /// True if the passing vector is equal to this vector.
        /// </summary>
        /// <param name="other">The other vector.</param>
        /// <returns>True if the passing vector is equal to this vector.</returns>
        public bool Equals(Vector other)
        {
            if (this == other)
                return true;
            else
                return this._x == other._x && this._y == other._y;
        }
        
        /// <summary>
        /// Returns a value that indicates whether the current instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">Object to make the comparison with.</param>
        /// <returns>true if the current instance is equal to the specified object; false otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is Vector)
                return Equals((Vector)obj);
            else
                return false;
        }

        /// <summary>
        /// Gets the hash code for this object. 
        /// </summary>
        /// <returns>Hash code for this object.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
