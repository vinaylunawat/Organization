namespace Framework.Business.Extension
{
    using System;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Defines the <see cref="StringExtensions" />.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Defines the _balanceLock.
        /// </summary>
        private static readonly object _balanceLock = new object();

        /// <summary>
        /// The ToGuidValue.
        /// </summary>
        /// <param name="source">The source<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
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

        /// <summary>
        /// The ToGuid.
        /// </summary>
        /// <param name="source">The source<see cref="string"/>.</param>
        /// <returns>The <see cref="Guid"/>.</returns>
        public static Guid ToGuid(this string source)
        {
            return Guid.Parse(ToGuidValue(source));
        }

        /// <summary>
        /// The ToLowerFirstCharacter.
        /// </summary>
        /// <param name="input">The input<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string ToLowerFirstCharacter(this string input)
        {
            var firstChar = input[0];
            if (firstChar >= 'A' && firstChar <= 'Z')
            {
                firstChar = (char)(firstChar + 32);
            }

            return firstChar + input.Substring(1);
        }

        /// <summary>
        /// The PermissionToFriendlyName.
        /// </summary>
        /// <param name="text">The text<see cref="string"/>.</param>
        /// <param name="delimiter">The delimiter<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string PermissionToFriendlyName(this string text, string delimiter = " ")
        {
            return string.Join(delimiter, Regex.Split(text, @"(?<!^)(?=[A-Z])"));
        }
    }
}
