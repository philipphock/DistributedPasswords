using DistributedPasswordsWPF.controller;
using DistributedPasswordsWPF.model;
using DistributedPasswordsWPF.model.util;
using NHotkey;
using NHotkey.Wpf;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;

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
            PasswordSystem.Instance.Init();

            _tray = new Tray(this);


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
Application.Current.Dispatcher.Invoke(
    () =>
    {
               
    });
*/
