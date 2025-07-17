using System;
using System.Text;

namespace com.ih.util.generic.Randoms
{
    public static class RandomUtil
    {
        public static string GeneratePassword()
        {
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ+@%*#!_=$%-?;";

            var sb = new StringBuilder();
            var rnd = new Random();

            for (int i = 0; i < 15; i++)
            {
                int index = rnd.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }

        public static string GenerateValidateCode()
        {
            const string chars = "0123456789";

            var sb = new StringBuilder();
            var rnd = new Random();

            for (int i = 0; i < 6; i++)
            {
                int index = rnd.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }
    }
}