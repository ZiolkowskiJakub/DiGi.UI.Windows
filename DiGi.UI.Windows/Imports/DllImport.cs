using System;
using System.Runtime.InteropServices;

namespace DiGi.UI.Windows
{
    public static partial class Imports
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
    }
}