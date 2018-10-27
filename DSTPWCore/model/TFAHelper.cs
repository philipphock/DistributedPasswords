using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace DstPasswordsCore.model
{
    public class TFAHelper
    {

        public static string AddPadding(string value)
        {
            return string.Concat(new String('0', Math.Max((6 - value.Length), 0)), value);
        }

        public static string TryParseUrl(string content)
        {
            string param = content;
            try
            {
                Uri uri = new Uri(param);
                param = HttpUtility.ParseQueryString(uri.Query).Get("secret");
                if (string.IsNullOrEmpty(param))
                {
                    param = content;

                }
            }
            catch (Exception)
            {

            }
            return param;
        }
    }
}
