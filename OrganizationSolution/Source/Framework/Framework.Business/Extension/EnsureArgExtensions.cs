namespace Framework.Business.Extension
{
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;

    public static class EnsureArgExtensions
    {
        public static void HasItems<T>(IEnumerable<T> items, string paramName = null)
        {
            EnsureArg.IsTrue(items.Any(), paramName, options => options.WithMessage("Empty collection is not allowed."));
        }
    }
}
