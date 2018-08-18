using DstPasswordsCore.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Auth2FA.GenerateOTP("test");

        }
    }
}
