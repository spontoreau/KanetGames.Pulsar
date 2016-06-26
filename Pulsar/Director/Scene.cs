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
using System.Collections.Generic;

namespace Pulsar.Director
{
    /// <summary>
    /// Game scene.
    /// This type of object is litteraly a collection of shot (like in cinema). 
    /// </summary>
    public class Scene
    {
        /// <summary>
        /// True if Scene load content is heavy (so the scene manager will load it in async mode and allow developper to show a loading screen).
        /// </summary>
        public bool IsHeavyLoad { get; private set; }

        /// <summary>
        /// List of type corresponding to all shot which game scene manager need to include in the scene
        /// </summary>
        public List<Type> ShotTypes { get; private set; }

        /// <summary>
        /// Create a new instance of the Scene
        /// </summary>
        public Scene()
        {

        }

        /// <summary>
        /// Get the shot list corresponding to the scene
        /// </summary>
        /// <returns>The shot list</returns>
        public List<Shot> GetShots()
        {
            List<Shot> shots = new List<Shot>();

            foreach (Type type in ShotTypes)
            {
                object obj = Activator.CreateInstance(type);

                if (obj is Shot)// TODO WTF
                {
                    throw new DirectorEngineException(typeof(Scene).FullName, string.Format(Pulsar.Resources.Exceptions.Director.InvalidSceneType, type.FullName));
                }
                else
                {
                    Shot shot = (Shot)obj;
                    shots.Add(shot);
                }
            }

            return shots;
        }
    }
}
