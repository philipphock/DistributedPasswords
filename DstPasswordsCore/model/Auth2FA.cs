using DstPasswordsCore.model.util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DstPasswordsCore.model
{
    public class Auth2FA
    {
        public static int GenerateOTP(string secret, long interval = 30, int digits = 6)
        {
            
            long Timestamp = (new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds())/ interval;
            
            string hexstring = Timestamp.ToString("X");
            while (hexstring.Length < 16)
            {
                hexstring = "0" + hexstring;
            }

            byte[] timebin = BinaryHelper.Unhexlify(hexstring);
            byte[] secretBytes = Base32.ToBytes(secret);
            HMACSHA1 hmac = new HMACSHA1(secretBytes);

            byte[] hash = hmac.ComputeHash(timebin);


            
            int offset = hash[hash.Length - 1] & 0xf;

            int binary = ((hash[offset] & 0x7f) << 24) | ((hash[offset + 1] & 0xff) << 16) | ((hash[offset + 2] & 0xff) << 8) | (hash[offset + 3] & 0xff);
            int result = (int) (binary % Math.Pow(10, digits));

            
            return result;
        }

        public static Auth2FAUpdateEvent GenerateRenewableOTP(string secret, long interval=30, int digits = 6)
        {
            return new Auth2FAUpdateEvent(secret, interval, digits);
        }

       
    
        

    }
}


