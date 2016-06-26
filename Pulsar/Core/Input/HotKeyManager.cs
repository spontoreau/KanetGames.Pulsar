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

using SFML.Window;

namespace Pulsar.Core.Input
{
    /// <summary>
    /// Manage HotKey
    /// </summary>
    public sealed class HotKeyManager
    {
        /// <summary>
        /// True if hot key is valid
        /// </summary>
        /// <param name="hotKey">The hotkey to test</param>
        /// <returns></returns>
        public static bool IsHotKeyValid(HotKey hotKey)
        {
            return ((hotKey.IsShift == Keyboard.IsKeyPressed(Keyboard.Key.LShift) || hotKey.IsShift == Keyboard.IsKeyPressed(Keyboard.Key.RShift))
                && (hotKey.IsCtrl == Keyboard.IsKeyPressed(Keyboard.Key.LControl) || hotKey.IsCtrl == Keyboard.IsKeyPressed(Keyboard.Key.RControl))
                && (hotKey.IsAlt == Keyboard.IsKeyPressed(Keyboard.Key.RAlt) || hotKey.IsAlt == Keyboard.IsKeyPressed(Keyboard.Key.LAlt))
                && Keyboard.IsKeyPressed(hotKey.Key));
        }
    }
}
