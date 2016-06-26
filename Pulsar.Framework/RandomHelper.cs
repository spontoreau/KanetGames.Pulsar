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
    /// Extension of the Microsoft Random class.
    /// </summary>
    public static class RandomHelper
    {
        /// <summary>
        /// Static instance of a Random class.
        /// </summary>
        private static Random _random = new Random();

        /// <summary>
        /// Return a non negative int.
        /// </summary>
        public static int NextInt()
        {
            return _random.Next();
        }

        /// <summary>
        /// Return a non negative int less than maxValue.
        /// </summary>
        public static int NextInt(int maxValue)
        {
            return _random.Next(maxValue);
        }

        /// <summary>
        /// Return a non negative int in a specific range.
        /// </summary>
        public static int NextInt(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }

        /// <summary>
        /// Return a random boolean.
        /// </summary>
        public static bool NextBool()
        {
            return NextInt(2) == 1;
        }

        /// <summary>
        /// Return a float between 0.0f and 1.0f.
        /// </summary>
        public static float NextFloat()
        {
            return (float)_random.NextDouble();
        }

        /// <summary>
        /// Return a float less than maxValue.
        /// </summary>
        public static float NextFloat(float maxValue)
        {
            return maxValue * NextFloat();
        }

        /// <summary>
        /// Return a float between minValue and maxValue.
        /// </summary>
        public static float NextFloat(float minValue, float maxValue)
        {
            return (maxValue - minValue) * NextFloat() + minValue;
        }

        /// <summary>
        /// Return a random radians angle
        /// </summary>
        /// <returns></returns>
        public static float NextRadiansAngle()
        {
            return NextFloat(-MathHelper.Pi, (float)MathHelper.Pi);
        }

        /// <summary>
        /// Return a random Normalize Vector2.
        /// </summary>
        public static Vector NextVector()
        {
            return Vector.Polar(1.0f, RandomHelper.NextRadiansAngle());
        }

        /// <summary>
        /// Return a random T from specific params.
        /// </summary>
        public static T Choose<T>(params T[] values)
        {
            int index = NextInt(values.Length);

            return values[index];
        }
    }
}
