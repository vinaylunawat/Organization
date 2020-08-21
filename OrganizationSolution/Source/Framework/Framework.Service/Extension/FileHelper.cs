namespace Framework.Service.Extension
{
    using EnsureThat;
    using System;

    public static class FileHelper
    {
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
