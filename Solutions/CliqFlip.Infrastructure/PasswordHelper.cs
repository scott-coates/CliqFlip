using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace CliqFlip.Infrastructure
{
    public static class PasswordHelper
    {
        private const int ITERATIONS = 5000;
        private const int HASH_SIZE = 32;

        // QUESTION: Isn't a salt and password created at the same time? isn't salt an output of rfc2898...?
        // http://code.google.com/p/stackid/source/browse/OpenIdProvider/Current.cs#1236
        // A salt is not the output of rfc2898. 
        // A salt value is used to make the password's hash unique even if two password are the same.
        // EXAMPLE: 
        // PASSWORD: 12345      SALT: k_fan@-a      OUTPUT: _fn3qq
        // PASSWORD: 12345      SALT: &gd%asf2      OUTPUT: @bd#ka_
        //
        // Using a salt protects us from a rainbow table attack
        // http://stackoverflow.com/a/421081/358007
        // Basically, it makes it more time consuming for someone trying to figure out our password
        // because each one is uniques



        /// <summary>
        /// Generates a random salt value
        /// </summary>
        /// <param name="bytes">The size of the salt value</param>
        /// <returns>A unique salt value</returns>
        public static String GenerateSalt(int bytes)
        {
            using (var provider = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[bytes];
                provider.GetBytes(salt);
                return Convert.ToBase64String(salt);
            }
        }

        public static String GetPasswordHash(String password, String salt)
        {
            var saltBytes = Encoding.UTF8.GetBytes(salt);
            using (var rfc2898 = new Rfc2898DeriveBytes(password, saltBytes, ITERATIONS))
            {
                return Convert.ToBase64String(rfc2898.GetBytes(HASH_SIZE));
            }
        }
    }
}