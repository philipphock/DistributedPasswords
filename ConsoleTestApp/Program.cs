using DstPasswordsCore.model;
using System;
using System.Threading;

namespace ConsoleTestApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Auth2FAUpdateEvent e = Auth2FA.GenerateReneableOTP("NBSWY3DP",1);


            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = false;

                Console.ReadKey();
                e.UnsubscribeAll();
                Console.WriteLine("byebye");
            
            }).Start();

            e.Subscribe((s, i) =>
            {
                Console.WriteLine(i);
            });

            e.UnsubscribeAll();

            e.Subscribe((s, i) =>
            {
                Console.WriteLine("->"+i);
            });

        }
            
   
       
    }
}
