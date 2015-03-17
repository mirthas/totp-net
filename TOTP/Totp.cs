using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


[assembly: InternalsVisibleTo("TotpTests")]

namespace TOTP
{    
    public class Totp
    {
        
        internal byte[] key;
        private Int32 t1 = 30;
        private int digits = 6;
        private HMACSHA1 hmac;
       

        public Totp(string base32string)
        {
            key = FromBase32String(base32string);
            hmac = new HMACSHA1(key);
        }

        public Totp(string base32string, Int32 t1, int digits) : this(base32string)
        {
            this.t1 = t1;
            this.digits = digits;
        }



       private byte[] FromBase32String(string base32string)
        {

            return Base32.FromBase32String(base32string);
        }

       public int getCode()
       {

           UInt64 unixTimestamp = (UInt64)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

           return getCode(unixTimestamp);
       }

       public int getCode(UInt64 timeStamp)
       {

           UInt64 message = timeStamp / (UInt64)t1;
           byte[] msg_byte = BitConverter.GetBytes(message);
           if (BitConverter.IsLittleEndian)
                Array.Reverse(msg_byte, 0, msg_byte.Length);
           byte[] hash = hmac.ComputeHash(msg_byte);
           int offset = (hash[hash.Length - 1] & 0xf);
           int i = ((hash[offset] & 0x7f) << 24) | ((hash[offset + 1] & 0xff) << 16) | ((hash[offset + 2] & 0xff) << 8) | (hash[offset + 3] & 0xff);
           int code = i % (int) Math.Pow(10,digits);
           return code;
       }

       public String getCodeString()
       {
           UInt64 unixTimestamp = (UInt64)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

           return getCodeString(unixTimestamp);
       }
       public String getCodeString(UInt64 timeStamp)
       {
           String ret = getCode(timeStamp) + "";
           return ret.PadLeft(digits,'0');
       }

       public static bool CheckBase32(string base32string)
       {
           try {
               byte[] tmp = Base32.FromBase32String(base32string);
               return tmp != null;
           }
           catch {
               return false;
           }

           
       }

    }
}
