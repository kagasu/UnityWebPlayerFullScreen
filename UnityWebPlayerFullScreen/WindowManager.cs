using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnityWebPlayerFullScreen
{
    class WindowManager
    {
        const int WS_BORDER = 8388608;
        const int WS_DLGFRAME = 4194304;
        const int WS_CAPTION = WS_BORDER | WS_DLGFRAME;
        const int WS_SYSMENU = 524288;
        const int WS_THICKFRAME = 262144;
        const int WS_MINIMIZE = 536870912;
        const int WS_MAXIMIZEBOX = 65536;
        const int GWL_STYLE = -16;
        const int GWL_EXSTYLE = -20;
        const int SWP_FRAMECHANGED = 0x20;

        [DllImport("USER32.DLL")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("USER32.DLL")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("USER32.DLL")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

        public WindowManager()
        {

        }

        public static void ChangeBorderless(IntPtr hWnd)
        {
            var style = GetWindowLong(hWnd, GWL_STYLE);
            style &= ~WS_CAPTION & ~WS_SYSMENU & ~WS_THICKFRAME & ~WS_MINIMIZE & ~WS_MAXIMIZEBOX;

            SetWindowLong(hWnd, GWL_STYLE, style);
            style = GetWindowLong(hWnd, GWL_EXSTYLE);
            SetWindowLong(hWnd, GWL_EXSTYLE, style);
        }

        public static void ChangeFullScreen(IntPtr hWnd, string deviceName)
        {
            foreach (var screen in Screen.AllScreens)
            {
                if (screen.DeviceName == deviceName)
                {
                    SetWindowPos(hWnd, IntPtr.Zero, screen.Bounds.X, screen.Bounds.Y, screen.Bounds.Width, screen.Bounds.Height, SWP_FRAMECHANGED);
                    break;
                }
            }
        }
    }
}
