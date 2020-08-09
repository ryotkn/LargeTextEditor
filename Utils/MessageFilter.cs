using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LargeTextEditor
{
    /// <summary>
    /// https://stackoverflow.com/questions/11034348/mouse-wheel-event-to-work-with-hovered-control
    /// edited Oct 6 '17 at 5:25
    /// Arvo Bowen
    /// answered May 14 '16 at 9:22
    /// Ali.DM
    /// </summary>
    class MessageFilter : IMessageFilter
    {
        private const int WM_MOUSEWHEEL = 0x020A;
        private const int WM_MOUSEHWHEEL = 0x020E;

        [DllImport("user32.dll")]
        static extern IntPtr WindowFromPoint(Point p);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_MOUSEWHEEL:
                case WM_MOUSEHWHEEL:
                    IntPtr hControlUnderMouse = WindowFromPoint(new Point((int)m.LParam));
                    if (hControlUnderMouse == m.HWnd)
                    {
                        //Do nothing because it's already headed for the right control
                        return false;
                    }
                    else
                    {
                        //Send the scroll message to the control under the mouse
                        uint u = Convert.ToUInt32(m.Msg);
                        SendMessage(hControlUnderMouse, u, m.WParam, m.LParam);
                        return true;
                    }
                default:
                    return false;
            }
        }
    }
}
