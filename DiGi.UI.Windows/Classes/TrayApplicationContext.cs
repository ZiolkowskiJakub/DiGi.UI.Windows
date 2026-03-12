using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

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
            // Initialize the window only once
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
            // If you want the window to auto-hide when focus is lost (like a popup)
            // Check if it's not already hiding to avoid recursion
            if (window != null && window.IsVisible)
            {
                window.Hide();
            }
        }

        private void OnWindowClosing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            // If we are not exiting the entire app, we just hide the window
            if (!isExiting)
            {
                e.Cancel = true;
                window?.Hide();
            }
            // Otherwise, the window will close normally
        }

        private void ExitApplication()
        {
            isExiting = true;

            // Cleanup NotifyIcon
            if (NotifyIcon != null)
            {
                NotifyIcon.Visible = false;
                NotifyIcon.Dispose();
            }

            // Properly close the window to allow for any internal cleanup
            if (window != null)
            {
                window.Closing -= OnWindowClosing; // Detach to avoid loop
                window.Close();
            }

            System.Windows.Application.Current.Shutdown();
        }
    }
}
