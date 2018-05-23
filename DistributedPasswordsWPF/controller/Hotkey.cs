using DistributedPasswordsWPF.model;
using DistributedPasswordsWPF.model.util;
using NHotkey;
using NHotkey.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using System.Windows.Forms;
using System.Windows.Input;

namespace DistributedPasswordsWPF.controller
{
    class Hotkey
    {
        private System.Timers.Timer _timer = new System.Timers.Timer(500);
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

            
            lastHotkey = t;

        }

        


        private void _elapsedHotkeyTime(object source, ElapsedEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(
            () =>
            {
                if (hotkeyCounter == 1 || hotkeyCounter == 2)
                {
                    string data = System.Windows.Clipboard.GetData(System.Windows.DataFormats.Text) as string;
                    data = URL.URLize(data);
                    PasswordSystem.Instance.TrySelect(data);
                    string el = null;
                    if (hotkeyCounter == 1)
                    {
                        el = PasswordSystem.Instance.GetUsernameFromSelection();
                    }
                    if (hotkeyCounter == 2)
                    {
                        el = PasswordSystem.Instance.GetPasswordFromSelection();
                    }
                    if (el != null)
                    {
                        foreach (char c in el)
                        {
                            if ("~^+(){}[]%".Contains(c))
                            {
                                SendKeys.SendWait("{" + c + "}");
                            }
                            else
                            {
                                SendKeys.SendWait(c.ToString());
                            }
                        }
                    }
                }
                
                
                if (hotkeyCounter == 3)
                {
                    Router.instance.ShowMain();
                }
            });
            

            hotkeyCounter = 0;
            _timer.Stop();
        }


        private void HotkeyManager_HotkeyAlreadyRegistered(object sender, HotkeyAlreadyRegisteredEventArgs e)
        {
            System.Windows.MessageBox.Show(string.Format("The hotkey {0} is already registered by another application", e.Name));
        }

        

        
    }
}
