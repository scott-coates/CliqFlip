﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace CliqFlip.Infrastructure
{
    public static class PasswordHelper
    {
        public static String GenerateSalt(int bytes)
        {
            byte[] salt = new byte[8];
            new RNGCryptoServiceProvider().GetBytes(salt);           
            return Convert.ToBase64String(salt);
        }

        public static String GetPasswordHash(String password, String salt)
        {
            int iteration = 5000;
            int bytes = 32;
            var byteSalt = Encoding.UTF8.GetBytes(salt);
            var rfc2898 = new Rfc2898DeriveBytes(password, byteSalt, iteration);
            return Convert.ToBase64String(rfc2898.GetBytes(bytes));
        }
    }
}