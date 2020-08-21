namespace Framework.Service.Extension
{
    using EnsureThat;
    using System;

    /// <summary>
    /// Defines the <see cref="FileHelper" />.
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// The NormalizeFolderPath.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string NormalizeFolderPath(string path)
        {
            EnsureArg.IsNotNullOrWhiteSpace(path, nameof(path));

            var normalizedPath = path.Replace(@"\", "/", StringComparison.InvariantCultureIgnoreCase);
            var hasTrailingSlash = normalizedPath.EndsWith("/", StringComparison.InvariantCultureIgnoreCase);
            if (!hasTrailingSlash)
            {
                normalizedPath += '/';
            }

            return normalizedPath;
        }
    }
}
