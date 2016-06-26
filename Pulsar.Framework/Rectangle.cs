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
    [Serializable()]
    public sealed class Rectangle : ICloneable, IEquatable<Rectangle>
    {
        private float _x;
        private float _y;
        private float _width;
        private float _height;

        /// <summary>
        /// get an empty Rectangle
        /// </summary>
        public static Rectangle Empty
        {
            get
            {
                return new Rectangle(0.0f, 0.0f, 0.0f, 0.0f);
            }
        }

        /// <summary>
        /// True if the rectangle is Empty
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return this._x == 0.0f && this._y == 0.0f && this._width == 0.0f && this._height == 0.0f;
            }
        }

        /// <summary>
        /// Get the current X coordinate of the Rectangle
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
        /// Get the current Y coordinate of the Rectangle
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
        /// Get the current Width coordinate of the Rectangle
        /// </summary>
        public float Width
        {
            get
            {
                return this._width;
            }
            set
            {
                this._width = value;
            }
        }

        /// <summary>
        /// Get the current Height coordinate of the Rectangle
        /// </summary>
        public float Height
        {
            get
            {
                return this._height;
            }
            set
            {
                this._height = value;
            }
        }

        /// <summary>
        /// Get the position of the Rectangle
        /// </summary>
        public Vector Position
        {
            get
            {
                return new Vector(this._x, this._y);
            }
        }

        /// <summary>
        /// Get the centrer
        /// </summary>
        public Vector Center
        {
            get
            {
                return new Vector(Right / 2, Bottom / 2);
            }
        }

        /// <summary>
        /// Get X coordinate of the Rectangle Left
        /// </summary>
        public float Left
        {
            get
            {
                return this._x;
            }
        }

        /// <summary>
        /// Get X coordinate of the Rectangle Right
        /// </summary>
        public float Right
        {
            get
            {
                return this._x + this._width;
            }
        }

        /// <summary>
        /// Get Y coordinate of the Rectangle Top
        /// </summary>
        public float Top
        {
            get
            {
                return this._y;
            }
        }

        /// <summary>
        /// Get Y coordinate of the Rectangle Bottom
        /// </summary>
        public float Bottom
        {
            get
            {
                return this._y + this._height;
            }
        }

        /// <summary>
        /// Create a new instance of Rectangle.
        /// </summary>
        public Rectangle()
        {

        }

        /// <summary>
        /// Create a new instance of Rectangle.
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="width">Width size</param>
        /// <param name="height">Height size</param>
        public Rectangle(float x, float y, float width, float height)
        {
            this._x = x;
            this._y = y;
            this._width = width;
            this._height = height;
        }

        /// <summary>
        /// Create a new instance of Rectangle
        /// </summary>
        /// <param name="position">Position vector</param>
        /// <param name="size">Size vector</param>
        public Rectangle(Vector position, Vector size)
            : this(position.X, position.Y, size.X, size.Y)
        {

        }

        /// <summary>
        /// True if the Rectangle contains a vector.
        /// </summary>
        /// <param name="v">The vector.</param>
        /// <returns>True if the Rectangle contains a vector.</returns>
        public bool Contains(Vector v)
        {
            return (v.X >= this._x) && (v.X < this.Right) && (v.Y >= this._y) && (v.Y < this.Bottom);
        }

        /// <summary>
        /// True if the Rectangle contains an other Rectangle.
        /// </summary>
        /// <param name="r">Other Rectangle.</param>
        /// <returns>True if the Rectangle contains an other Rectangle.</returns>
        public bool Contains(Rectangle r)
        {
            return (r._x >= this._x) && (r.Right < this.Right) && (r._y >= this._y) && (r.Bottom < this.Bottom);
        }

        /// <summary>
        /// True if Rectangle intersects an other Rectangle.
        /// </summary>
        /// <param name="r">Other Rectangle.</param>
        /// <returns>True if a Rectangle intersects this Rectangle.</returns>
        public bool Intersects(Rectangle r)
        {
            return (r._x < this.Right) && (r.Right > this._x) && (r._y < this.Bottom) && (r.Bottom > this._y);
        }

        /// <summary>
        /// Get the union representation of 2 rectangles.
        /// </summary>
        /// <param name="r1">Rectangle 1.</param>
        /// <param name="r2">Rectangle 2.</param>
        /// <returns>Rectangle representation of the union.</returns>
        public static Rectangle Union(Rectangle r1, Rectangle r2)
        {
            float x = (r1._x < r2._x) ? r1._x : r2._x;
            float y = (r1._y < r2._y) ? r1._y : r2._y;
            float width = ((r1.Right > r2.Right) ? r1.Right : r2.Right) - x;
            float height = ((r1.Bottom > r2.Bottom) ? r1.Bottom : r2.Bottom) - y;
            return new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// Clone the Rectangle.
        /// </summary>
        /// <returns>Cloned Rectangle.</returns>
        public object Clone()
        {
            return new Rectangle(this._x, this._y, this._width, this._height);
        }

        /// <summary>
        /// Returns a value that indicates whether the current instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">Object to make the comparison with.</param>
        /// <returns>true if the current instance is equal to the specified object; false otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is Rectangle)
                return Equals((Rectangle)obj);
            else
                return false;
        }

        /// <summary>
        /// True if the passing Rectangle is equal to this Rectangle.
        /// </summary>
        /// <param name="other">The other Rectangle.</param>
        /// <returns>True if the passing Rectangle is equal to this Rectangle.</returns>
        public bool Equals(Rectangle other)
        {
            if (this == other)
                return true;
            else
                return this._x == other._x && this._y == other._y && this._width == other._width && this._height == other._height;
        }

        /// <summary>
        /// True if Rectangle 2 is equal to Rectangle 1.
        /// </summary>
        /// <param name="r1">Rectangle 1.</param>
        /// <param name="r2">Rectangle 2.</param>
        /// <returns>True if Rectangle 2 is equal to Rectangle 1.</returns>
        public static bool operator ==(Rectangle r1, Rectangle r2)
        {
            return r1.Equals(2);
        }

        /// <summary>
        /// True if Rectangle 2 isn't equal to Rectangle 1.
        /// </summary>
        /// <param name="r1">Rectangle 1.</param>
        /// <param name="r2">Rectangle 2.</param>
        /// <returns>True if Rectangle 2 isn't equal to Rectangle 1.</returns>
        public static bool operator !=(Rectangle r1, Rectangle r2)
        {
            return !r1.Equals(r2);
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
