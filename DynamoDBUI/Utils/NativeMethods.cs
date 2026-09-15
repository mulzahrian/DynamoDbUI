using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DynamoDBUI.Utils
{
    internal static class NativeMethods
    {
        private const int WM_SETREDRAW = 0x000B;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, bool wParam, int lParam);

        public static void SuspendDrawing(Control control)
        {
            if (control.IsHandleCreated)
                SendMessage(control.Handle, WM_SETREDRAW, false, 0);
        }

        public static void ResumeDrawing(Control control)
        {
            if (control.IsHandleCreated)
            {
                SendMessage(control.Handle, WM_SETREDRAW, true, 0);
                control.Invalidate();
            }
        }
    }
}