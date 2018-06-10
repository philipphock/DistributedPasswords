using DistributedPasswordsWPF.controller;
using DistributedPasswordsWPF.model;
using System;
using System.Windows;

namespace DistributedPasswordsWPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly Hotkey _hotkey;
        private readonly Tray _tray;

        public MainWindow()
        {
            _hotkey = new Hotkey();
           

            InitializeComponent();

            Router.instance.Init(this);
            bool r = PasswordSystem.Instance.Init();
            if (r)
            {
                Router.instance.DisplayPage(Router.Pages.PathSettings);
            }
            else
            {
                Router.instance.DisplayPage(Router.Pages.Unlock);
            }


            _tray = new Tray(this);

            this.StateChanged += Window_StateChanged;

        }




        private void Window_StateChanged(object sender, EventArgs e)
        {
            switch (this.WindowState)
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



        private void Window_Closed(object sender, EventArgs e)
        {

            _tray.Dispose();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
    }
}

/*

*/
