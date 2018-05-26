﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedPasswordsWPF.debug
{
    class DEBUG
    {
        public static bool enabled = true;

        public static void Print(string origin, params object[] o)
        {
            if (!enabled) return;
            Debug.WriteLine(string.Concat(origin, ": ", string.Join(" ",o)));
        }
        public static void Print(Type t, params object[] o)
        {
            DEBUG.Print(t.ToString(), o);
        }

        

    }
}
