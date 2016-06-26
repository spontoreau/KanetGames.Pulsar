using Pulsar.Graphics.Gui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsar.Graphics.Gui
{
    public class GuiManager
    {
        internal static Control CurrentFocus
        {
            get
            {
                return _focusControl;
            }
        }
        private static Control _focusControl;

        internal static Control OverControl;

        internal static void SetFocusTo(Control control)
        {
            _focusControl = control;
        }
    }
}
