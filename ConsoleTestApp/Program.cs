using DstPasswordsCore.model;
using System;
using System.Threading;

namespace ConsoleTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //NBSWY3DP
            try
            {
                int i = Auth2FA.GenerateOTP(";");
                Console.WriteLine(i);

            }
            catch (Exception e)
            {
                Console.WriteLine("ERR");

            }
            Console.ReadKey();

            /*
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
            */
        }



    }
}
