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

using System.Collections.Generic;
using System.Linq;

namespace Pulsar.Core.Commands
{
    /// <summary>
    /// Execute command process
    /// </summary>
    public static class CommandProcess
    {
        /// <summary>
        /// Command history
        /// </summary>
        public static CommandHistory History { get; set; }

        /// <summary>
        /// Commands avaible
        /// </summary>
        public static List<ICommand> Commands { get; set; }

        /// <summary>
        /// Create the static instance
        /// </summary>
        static CommandProcess()
        {
            History = new CommandHistory();
            Commands = new List<ICommand>();
        }

        /// <summary>
        /// Handle a command
        /// </summary>
        /// <param name="command">Command to handle</param>
        /// <returns>Command process response</returns>
        public static List<string> Handle(string command)
        {
            History.Add(command);
            command = command.Trim();
            if (!string.IsNullOrEmpty(command))
            {
                CommandCategory category = ParseCommandCategory(ref command);

                if (category != CommandCategory.None)
                {
                    if(!string.IsNullOrEmpty(command))
                    {
                        string name = ParseCommandName(ref command);

                        ICommand iCommand = (from c in Commands where c.Name.ToUpper() == name.ToUpper() && c.Category == category select c).FirstOrDefault();
                        if (iCommand != null)
                        {
                            string[] args = ParseCommandArgs(name, command);
                            return iCommand.Execute(ref args);
                        }
                        else
                        {
                            return new List<string> { "Unknow command" };//TODO remove string from the code
                        }
                    }
                    else
                    {
                        return new List<string> { "Empty command name" };//TODO remove string from the code
                    }
                }
                else
                {
                    return new List<string> { "Unknow category" };//TODO remove string from the code
                }
            }
            else
            {
                return new List<string> { "Empty command" };//TODO remove string from the code
            }
        }

        /// <summary>
        /// Parse command category
        /// </summary>
        /// <param name="command">Command to parse</param>
        /// <returns>The command category</returns>
        private static CommandCategory ParseCommandCategory(ref string command)
        {
            string[] commandArray = command.Split(' ');
            string category = commandArray[0];
            command = command.Replace(category, "");

            switch (category.ToUpper())
            {
                case "AUDIO":
                    return CommandCategory.Audio;
                case "CONSOLE" :
                    return CommandCategory.Console;
                case "DEBUG":
                    return CommandCategory.Debug;
                case "GFX":
                    return CommandCategory.Gfx;
                case "SYSTEM":
                    return CommandCategory.System;
                default :
                    return CommandCategory.None;
            }

        }

        /// <summary>
        /// Parse command name
        /// </summary>
        /// <param name="command">Command to parse</param>
        /// <returns>The command name</returns>
        private static string ParseCommandName(ref string command)
        {
            command = command.Trim();
            string[] commandArray = command.Split(' ');
            string name = commandArray[0];
            command = command.Replace(name, "");
            return name;
        }

        /// <summary>
        /// Parse command arguments
        /// </summary>
        /// <param name="command">Command to parse</param>
        /// <returns>The command arguments</returns>
        private static string[] ParseCommandArgs(string name, string command)
        {
            command = command.Trim();
            return command.Split(' ');
        }
    }
}
