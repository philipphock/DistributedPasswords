using DstPasswordsCore.model;
using DstPasswordsCore.model.util;
using NHotkey;
using NHotkey.Wpf;
using System;
using System.Linq;
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
        private string lastUsername = "";

        public Hotkey()
        {
            HotkeyManager.HotkeyAlreadyRegistered += HotkeyManager_HotkeyAlreadyRegistered;
            try
            {
                HotkeyManager.Current.AddOrReplace("hit", Key.P, ModifierKeys.Control | ModifierKeys.Alt, HotkeyCatched);
            }
            catch (HotkeyAlreadyRegisteredException e)
            {
                System.Windows.MessageBox.Show(string.Format("The hotkey {0} is already registered by another application", e.Name));
                Environment.Exit(1);
            }
            
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

        


        private  void _elapsedHotkeyTime(object source, ElapsedEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(
            () =>
            {
                if (hotkeyCounter == 1 || hotkeyCounter == 2 || hotkeyCounter == 3)
                {
                    string data = System.Windows.Clipboard.GetData(System.Windows.DataFormats.Text) as string;
                    data = URL.URLize(data);
                    if (!PasswordSystem.Instance.TrySelect(data))
                    {
                        return;
                    }
                    string el = null;
                    if (hotkeyCounter == 1)
                    {
                        lastUsername = data;
                        el = PasswordSystem.Instance.GetUsernameFromSelection();

                    }
                    if (hotkeyCounter == 2)
                    {
                        el = PasswordSystem.Instance.GetPasswordFromSelection();
                    }
                    if (hotkeyCounter == 3)
                    {
                        el = PasswordSystem.Instance.GetTFAFromSelection();
                        el = TFAHelper.TryParseUrl(el);
                        int eli = Auth2FA.GenerateOTP(el);
                        el = TFAHelper.AddPadding(""+eli);
                    }
                    
                    if (el != null)
                    {
                        if (PasswordSystem.Instance.Settings.CPY_PW)
                        {
                            Clipboard.SetData(DataFormats.Text, el);
                            SendKeys.SendWait("^v");

                            if (hotkeyCounter == 1)
                            {
                                Clipboard.SetData(DataFormats.Text, lastUsername);
                            }
                            if (hotkeyCounter == 2)
                            {
                                Clipboard.SetData(DataFormats.Text, lastUsername);
                            }
                            


                        }
                        else
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
                }
                
                
                if (hotkeyCounter == 4)
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
