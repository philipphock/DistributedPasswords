using DstPasswordsCore.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace DstPasswordsCore.model
{
    public class Auth2FAUpdateEvent
    {
        private readonly string secret;
        private readonly long interval;
        private readonly int digits;


        public event EventHandler<int> OTPChanged;

        private int _subscriber = 0;

        private System.Timers.Timer _checkTimer;
        
        public Auth2FAUpdateEvent(string secret, long interval, int digits)
        {
            this.secret = secret;
            this.interval = interval;
            this.digits = digits;

            _initTimer();

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Subscribe(EventHandler<int> onChange)
        {
            OTPChanged += onChange;
            _subscriber++;
            if (_subscriber == 1)
            {
                _initTimer();
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UnsubscribeAll()
        {
            if (_checkTimer != null)
            {

                _checkTimer.Enabled = false;
                _checkTimer.Stop();
                _checkTimer.Dispose();

                _checkTimer = null;

            }
            if (OTPChanged == null) return;
            foreach (Delegate d in OTPChanged.GetInvocationList())
            {
                OTPChanged -= (EventHandler<int>) d;
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Unsubscribe(EventHandler<int> onChange)
        {
            OTPChanged -= onChange;
            _subscriber--;
            if (_subscriber <= 0)
            {
                _subscriber = 0;
                _checkTimer.Enabled = false;
                _checkTimer.Stop();
                _checkTimer.Dispose();

                _checkTimer = null;

            }

        }

        private int prev = 0;

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void _initTimer()
        {
            if (_checkTimer != null)
            {
                _checkTimer.Enabled = false;
                _checkTimer.Stop();
                _checkTimer.Dispose();
               
            }
            _checkTimer = new System.Timers.Timer();
            _checkTimer.Interval = 1000;
            _checkTimer.Elapsed += (s, a) =>
            {
                int otp = Auth2FA.GenerateOTP(secret, interval, digits);
                if (otp != prev)
                {

                    OTPChanged?.Invoke(this,otp);
                    prev = otp;
                }
                
                
            };
            _checkTimer.Enabled = true;
        }

       
    }
}
