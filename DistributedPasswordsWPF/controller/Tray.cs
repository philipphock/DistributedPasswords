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

        
    }
}
