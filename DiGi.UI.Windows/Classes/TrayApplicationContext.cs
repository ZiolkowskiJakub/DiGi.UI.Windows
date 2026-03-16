using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Diagnostics;

namespace DiGi.UI.Windows.Classes
{
    public abstract class TrayApplicationContext<TWindow> : ApplicationContext where TWindow : Window
    {
        protected readonly NotifyIcon? NotifyIcon;
        private TWindow? window;
        private bool isExiting = false;

        public TrayApplicationContext(string text)
        {
            // 1. Setup Context Menu
            ContextMenuStrip contextMenuStrip = new ();
            contextMenuStrip.Items.Add("Open", null, (object? s, EventArgs e) => ShowWindow());
            contextMenuStrip.Items.Add("-");
            contextMenuStrip.Items.Add("Exit", null, (object? s, EventArgs e) => ExitApplication());

            // 2. Setup NotifyIcon
            NotifyIcon = new NotifyIcon()
            {
                Icon = Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath),
                ContextMenuStrip = contextMenuStrip,
                Visible = true,
                Text = text
            };

            NotifyIcon.DoubleClick += (object? s, EventArgs e) => ShowWindow();
        }

        protected abstract TWindow GetWindow();

        private void ShowWindow()
        {
            if (window == null)
            {
                window = GetWindow();
                window.Closing += OnWindowClosing;
                window.Deactivated += OnWindowDeactivated;
            }

            if (window.IsVisible)
            {
                if (window.WindowState == WindowState.Minimized)
                {
                    window.WindowState = WindowState.Normal;
                }
                window.Activate();
            }
            else
            {
                window.Show();
            }
        }

        private void OnWindowDeactivated(object? sender, EventArgs e)
        {
            if (window == null || !window.IsVisible)
            {
                return;
            }

            // Using Dispatcher to wait for the OS to update the Foreground Window
            window.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (window == null || !window.IsVisible)
                {
                    return;
                }

                IntPtr foregroundHandle = Imports.GetForegroundWindow();
                if (foregroundHandle == IntPtr.Zero)
                {
                    return;
                }

                Imports.GetWindowThreadProcessId(foregroundHandle, out uint foregroundProcessId);
                uint currentProcessId = (uint)Process.GetCurrentProcess().Id;

                // Only hide if the new active window belongs to a different process
                // (e.g. user clicked on Desktop, Taskbar or another App)
                if (foregroundProcessId != currentProcessId)
                {
                    window.Hide();
                }
            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        private void OnWindowClosing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isExiting)
            {
                e.Cancel = true;
                window?.Hide();
            }
        }

        private void ExitApplication()
        {
            isExiting = true;

            if (NotifyIcon != null)
            {
                NotifyIcon.Visible = false;
                NotifyIcon.Dispose();
            }

            if (window != null)
            {
                window.Closing -= OnWindowClosing;
                window.Close();
            }

            System.Windows.Application.Current.Shutdown();
        }
    }
}