namespace Framework.Test
{
    using System;
    using Xunit;

    /// <summary>
    /// Defines the <see cref="ITestFixture{TFixture}" />.
    /// </summary>
    /// <typeparam name="TFixture">.</typeparam>
    public interface ITestFixture<TFixture> : IClassFixture<TFixture>, IDisposable
         where TFixture : class
    {
        /// <summary>
        /// Gets the ServiceProvider.
        /// </summary>
        IServiceProvider ServiceProvider { get; }
    }
}
