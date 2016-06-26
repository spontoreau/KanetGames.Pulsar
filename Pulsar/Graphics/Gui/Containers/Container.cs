using Pulsar.Framework;
using Pulsar.Graphics.Gui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsar.Graphics.Gui.Containers
{
    public class Container
    {
        public List<Control> Controls { get; set; }

        public Rectangle Bounds { get; set; }

        /// <summary>
        /// Update the container
        /// </summary>
        /// <param name="gameTime">Time since last update</param>
        public virtual void Update(GameTime gameTime)
        {

        }
    }
}
