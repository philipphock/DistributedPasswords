using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DistributedPasswordsWPF.controller
{

    class Tray
    {
        private System.Windows.Forms.NotifyIcon notifyIcon = null;
        private readonly Window window;
        public Tray(Window window)
        {
            this.window = window;

            notifyIcon = new System.Windows.Forms.NotifyIcon
            {
                Icon = new System.Drawing.Icon("assets/img/tray.ico"),
                Visible = true
            };
            notifyIcon.Click += new System.EventHandler(_notifyIcon_Click);
            window.StateChanged += Window_StateChanged;

        }

        void _notifyIcon_Click(object sender, System.EventArgs e)
        {
            if (window.Visibility == Visibility.Visible)
            {
                Router.instance.HideMain();
            }
            else
            {
                Router.instance.ShowMain();
            }
        }

        public void Dispose()
        {
            notifyIcon.Dispose();
            notifyIcon = null;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            switch (window.WindowState)
            {
                case WindowState.Maximized:
                    break;
                case WindowState.Minimized:
                    Router.instance.HideMain();
                    break;
                case WindowState.Normal:

                    break;
            }
        }
    }
}
