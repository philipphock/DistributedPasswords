using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DstPasswordsCore.model.util
{
    public class URL
    {
        public static string URLize(string s)
        {
            if (s == null) return "";
            string ret=s;
            try
            {
                var url = new Uri(s);
                var fragments = url.Authority.Split('.');
                if (fragments.Length > 2)
                {
                    ret = fragments[fragments.Length - 2] + "." + fragments[fragments.Length - 1];
                }
                else
                {
                    ret = url.Authority;
                }
            }
            catch (UriFormatException)
            {
                //no uri
            }
            return ret;
        }
    }
}
