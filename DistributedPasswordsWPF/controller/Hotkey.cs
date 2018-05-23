using DistributedPasswordsWPF.model.util;
using NHotkey;
using NHotkey.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace DistributedPasswordsWPF.controller
{
    class Hotkey
    {
        private Timer _timer = new Timer(1000);
        private double lastHotkey = 0;
        private int hotkeyCounter = 0;

        public Hotkey()
        {
            HotkeyManager.HotkeyAlreadyRegistered += HotkeyManager_HotkeyAlreadyRegistered;
            HotkeyManager.Current.AddOrReplace("hit", Key.P, ModifierKeys.Control | ModifierKeys.Alt, HotkeyCatched);
            _timer.Elapsed += _elapsedHotkeyTime;

        }

        public void HotkeyCatched(object sender, HotkeyEventArgs e)
        {
            double t = Environment.TickCount;
            double dt = t - lastHotkey;

            _timer.Stop();

            _timer.Start();
            hotkeyCounter++;

            if (hotkeyCounter == 1)
            {
                string data = Clipboard.GetData(DataFormats.Text) as string;
                data = URL.URLize(data);
            }
            if (hotkeyCounter == 2)
            {
                string data = Clipboard.GetData(DataFormats.Text) as string;
                data = URL.URLize(data);
            }
            if (hotkeyCounter == 3)
            {
                Router.instance.ShowMain();
            }

            lastHotkey = t;

        }

        private void _elapsedHotkeyTime(object source, ElapsedEventArgs e)
        {
            hotkeyCounter = 0;
            _timer.Stop();
        }


        private void HotkeyManager_HotkeyAlreadyRegistered(object sender, HotkeyAlreadyRegisteredEventArgs e)
        {
            MessageBox.Show(string.Format("The hotkey {0} is already registered by another application", e.Name));
        }

        

        
    }
}
