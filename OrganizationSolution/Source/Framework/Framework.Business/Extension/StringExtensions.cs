namespace Framework.Business.Extension
{
    using System;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        private static readonly object _balanceLock = new object();

        public static string ToGuidValue(this string source)
        {
            byte[] hashedBytes;
            lock (_balanceLock)
            {
#pragma warning disable CA5351 // Do Not Use Broken Cryptographic Algorithms
                using (var hasher = new MD5CryptoServiceProvider())
#pragma warning restore CA5351 // Do Not Use Broken Cryptographic Algorithms
                {
                    var inputBytes = Encoding.ASCII.GetBytes(source);
                    hashedBytes = hasher.ComputeHash(inputBytes);
                }
            }

            var sb = new StringBuilder();
            foreach (var byteInHash in hashedBytes)
            {
                sb.Append(byteInHash.ToString("X2", CultureInfo.InvariantCulture));
            }

            return sb.ToString();
        }

        public static Guid ToGuid(this string source)
        {
            return Guid.Parse(ToGuidValue(source));
        }

        public static string ToLowerFirstCharacter(this string input)
        {
            var firstChar = input[0];
            if (firstChar >= 'A' && firstChar <= 'Z')
            {
                firstChar = (char)(firstChar + 32);
            }

            return firstChar + input.Substring(1);
        }

        public static string PermissionToFriendlyName(this string text, string delimiter = " ")
        {
            return string.Join(delimiter, Regex.Split(text, @"(?<!^)(?=[A-Z])"));
        }
    }
}
