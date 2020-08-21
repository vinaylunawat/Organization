namespace Framework.Business.Extension
{
    using EnsureThat;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="EnsureArgExtensions" />.
    /// </summary>
    public static class EnsureArgExtensions
    {
        /// <summary>
        /// The HasItems.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="items">The items<see cref="IEnumerable{T}"/>.</param>
        /// <param name="paramName">The paramName<see cref="string"/>.</param>
        public static void HasItems<T>(IEnumerable<T> items, string paramName = null)
        {
            EnsureArg.IsTrue(items.Any(), paramName, options => options.WithMessage("Empty collection is not allowed."));
        }
    }
}
