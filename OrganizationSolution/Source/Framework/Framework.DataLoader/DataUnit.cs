namespace Framework.DataLoader
{
    using EnsureThat;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="DataUnit" />.
    /// </summary>
    public abstract class DataUnit : IDataUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataUnit"/> class.
        /// </summary>
        /// <param name="logger">The logger<see cref="ILogger"/>.</param>
        protected DataUnit(ILogger logger)
        {
            EnsureArg.IsNotNull(logger, nameof(logger));

            Logger = logger;
        }

        /// <summary>
        /// Gets the Logger.
        /// </summary>
        public ILogger Logger { get; private set; }

        /// <summary>
        /// The LoadSeedDataAsync.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        public virtual Task LoadSeedDataAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// The LoadDemoDataAsync.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        public virtual Task LoadDemoDataAsync()
        {
            return Task.CompletedTask;
        }
    }
}
