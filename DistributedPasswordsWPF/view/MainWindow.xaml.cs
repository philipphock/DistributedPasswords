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
                    Router.instance.DisplayPage(Router.Pages.Main);
                    WinHandler?.Invoke(this, new WinState(WinState.State.Hide));
                    break;
                case WindowState.Normal:
                    WinHandler?.Invoke(this, new WinState(WinState.State.Show));
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

        public event EventHandler<WinState> WinHandler;

        public class WinState : EventArgs
        {
            public enum State
            {
                Hide, Show
            }
            private readonly State _State;
            public WinState(State s)
            {
                this._State = s;
            }

            public State WindowState
            {
                get
                {
                    return this._State;
                }
            }


        }

    }
}

/*

*/
