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

namespace Pulsar.Framework
{
    /// <summary>
    /// Extensions of the Microsoft Math class.
    /// </summary>
    public static class MathHelper
    {
        /// <summary>
        /// Pi contant
        /// </summary>
        public const float Pi = (float)System.Math.PI;

        /// <summary>
        /// 2 PI constant.
        /// </summary>
        public const float TwoPi = (float)(System.Math.PI * 2);

        /// <summary>
        /// E constant.
        /// </summary>
        public const float E = (float)(System.Math.E);

        /// <summary>
        /// Get the absolute value of a float.
        /// </summary>
        /// <param name="value">The absolute value of a float.</param>
        /// <returns>The absolute value of the float.</returns>
        public static float Abs(float value)
        {
            return System.Math.Abs(value);
        }

        /// <summary>
        /// get the Square root value of a float.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The square root value of a float.</returns>
        public static float Sqrt(float value)
        {
            return (float)System.Math.Sqrt((double)value);
        }

        /// <summary>
        /// Get the value pow 2
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>The value over 2</returns>
        public static float Square(float value)
        {
            return value * value;
        }

        /// <summary>
        /// get a value pow by an other value.
        /// </summary>
        /// <param name="x">X value.</param>
        /// <param name="y">Pow value.</param>
        /// <returns></returns>
        public static float Pow(float x, float y)
        {
            return (float)System.Math.Pow((double)x, (double)y);
        }

        /// <summary>
        /// Get the angular representation of a cosinus value.
        /// </summary>
        /// <param name="value">Cosinus value.</param>
        /// <returns>Angle corresponding to the cosinus.</returns>
        public static float Acos(float value)
        {
            return (float)System.Math.Acos((double)value);
        }

        /// <summary>
        /// Get the angular representation of a sinus value.
        /// </summary>
        /// <param name="value">Sinus value.</param>
        /// <returns>Angle corresponding to the sinus.</returns>
        public static float Asin(float value)
        {
            return (float)System.Math.Asin((double)value);
        }

        /// <summary>
        /// Get the angular representation of a tangent value.
        /// </summary>
        /// <param name="value">Tangent value.</param>
        /// <returns>Angle corresponding to the tangent.</returns>
        public static float Atan(float value)
        {
            return (float)System.Math.Atan((double)value);
        }

        /// <summary>
        /// Return the arctangent of quotient of y and x
        /// </summary>
        /// <param name="y">X coordinate</param>
        /// <param name="x">Y coordinate</param>
        /// <returns>Arctangent corresponding</returns>
        public static float Atan2(float y, float x)
        {
            return (float)System.Math.Atan2((double)y, (double)x);
        }

        /// <summary>
        /// Get the sinus value of an angle.
        /// </summary>
        /// <param name="value">The angle value.</param>
        /// <returns>Sinus corresponding to the angle value.</returns>
        public static float Sin(float value)
        {
            return (float)System.Math.Sin((double)value);
        }

        /// <summary>
        /// Get the tangent value of an angle.
        /// </summary>
        /// <param name="value">The angle value.</param>
        /// <returns>Tangent corresponding to the angle value.</returns>
        public static float Tan(float value)
        {
            return (float)System.Math.Tan((double)value);
        }

        /// <summary>
        /// Get the cosinus value of an angle.
        /// </summary>
        /// <param name="value">The angle value.</param>
        /// <returns>Cosinus corresponding to the angle value.</returns>
        public static float Cos(float value)
        {
            return (float)System.Math.Cos((double)value);
        }

        /// <summary>
        /// Get E pow value.
        /// </summary>
        /// <param name="value">Pow value.</param>
        /// <returns>E pow value.</returns>
        public static float Exp(float value)
        {
            return (float)System.Math.Exp((double)value);
        }

        /// <summary>
        /// get the Log E base of a value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Log E base a the value.</returns>
        public static float Log(float value)
        {
            return (float)System.Math.Log((double)value);
        }

        /// <summary>
        /// get the Log base 10 of a value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Log base 10 a the value.</returns>
        public static float Log10(float value)
        {
            return (float)System.Math.Log10((double)value);
        }

        /// <summary>
        /// Linearly interpolates between two values. Passing amount a value of 0 will cause value1 to be returned, a value of 1 will cause value2 to be returned.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of value2.</param>
        /// <returns>Lerp</returns>
        public static float Lerp(float value1, float value2, float amount)
        {
            return value1 + (value2 - value1) * amount;
        }

        /// <summary>
        /// Returns the greater of two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <returns>The greater value.</returns>
        public static float Max(float value1, float value2)
        {
            return (value1 > value2) ? value1 : value2;
        }

        /// <summary>
        /// Returns the lesser of two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <returns>The lesser value.</returns>
        public static float Min(float value1, float value2)
        {
            return (value1 < value2) ? value1 : value2;
        }

        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static float Clamp(float value, float min, float max)
        {
            if (value > max)
                return max;
            else if (value < min)
                return min;
            else
                return value;
        }

        /// <summary>
        /// Round a float value
        /// </summary>
        /// <param name="f">The float value</param>
        /// <returns>The float round value</returns>
        public static float Round(float f)
        {
            return (float)System.Math.Round((double)f);
        }

        /// <summary>
        /// Round a float value
        /// </summary>
        /// <param name="f">The float value</param>
        /// <param name="i">The int value after comma</param>
        /// <returns></returns>
        public static float Round(float f, int i)
        {
            return (float)System.Math.Round(f, i);
        }

        /// <summary>
        /// Return distance betwen two point coordinates
        /// </summary>
        /// <param name="x1">point 1 X coordinate.</param>
        /// <param name="y1">point 1 Y coordinate.</param>
        /// <param name="x2">point 2 X coordinate.</param>
        /// <param name="y2">point 2 Y coordinate.</param>
        /// <returns>Distance between the Point coordinates 1 and 2.</returns>
        public static float Distance(float x1, float y1, float x2, float y2)
        {
            return Sqrt(((x1 - x2) * (x1 - x2)) + ((y1 - y2) * (y1 - y2)));
        }

        /// <summary>
        /// Return angle of two point coordinates in radians.
        /// </summary>
        /// <param name="x1">point 1 X coordinate.</param>
        /// <param name="y1">point 1 Y coordinate.</param>
        /// <param name="x2">point 2 X coordinate.</param>
        /// <param name="y2">point 2 Y coordinate.</param>
        /// <returns>Angle in radian of of the point coordinates 1 and 2.</returns>
        public static float RadiansAngle(float x1, float y1, float x2, float y2)
        {
            return Atan2(x1 - x2, y2 - y1);
        }

        /// <summary>
        /// Convert radian angle into degree angle.
        /// </summary>
        /// <param name="radian">Radian angle.</param>
        /// <returns>Degree angle.</returns>
        public static float ToDegrees(float radians)
        {
            return radians * (180.0f / Pi);
        }

        /// <summary>
        /// Convert degree angle into radian angle.
        /// </summary>
        /// <param name="degree">Degree angle.</param>
        /// <returns>Radian angle.</returns>
        public static float ToRadians(float degrees)
        {
            return degrees * (Pi / 180.0f);
        }

        /// <summary>
        /// Return true if clockwise direction is the faster way to go from current angle to reach angle.
        /// </summary>
        /// <param name="currentAngle">Current angle</param>
        /// <param name="angleToReach">Angle to reach</param>
        /// <returns>True if clockwise direction is the faster way to go from current angle to reach angle.</returns>
        public static bool ClockWiseDirection(float currentAngle, float angleToReach)
        {
            float clockWiseDirection = angleToReach - currentAngle;

            bool isClockWiseDirection = true;

            if (clockWiseDirection > 0 && Abs(clockWiseDirection) <= 180) isClockWiseDirection = true;
            else if (clockWiseDirection > 0 && Abs(clockWiseDirection) > 180) isClockWiseDirection = false;
            else if (clockWiseDirection < 0 && Abs(clockWiseDirection) <= 180) isClockWiseDirection = false;
            else if (clockWiseDirection < 0 && Abs(clockWiseDirection) > 180) isClockWiseDirection = true;

            return isClockWiseDirection;
        }
    }
}
