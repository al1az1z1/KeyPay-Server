using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace KeyPay.Common.Helpers
{
    public static class Utilities
    {
        public static void CreatePasswordhash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using (var hamc = new System.Security.Cryptography.HMACSHA512())
            {
                //password salt به صورت key
                passwordSalt = hamc.Key;
                //پسورد رو هشش کن در اصطلاح کامپیوتش کن
                passwordHash = hamc.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }
    }
}
