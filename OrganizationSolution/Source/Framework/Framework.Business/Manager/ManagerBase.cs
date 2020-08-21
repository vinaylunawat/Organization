namespace Framework.Business.Manager
{
    using EnsureThat;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Defines the <see cref="ManagerBase" />.
    /// </summary>
    public abstract class ManagerBase : IManagerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerBase"/> class.
        /// </summary>
        /// <param name="logger">The logger<see cref="ILogger"/>.</param>
        protected ManagerBase(ILogger logger)
        {
            EnsureArg.IsNotNull(logger, nameof(logger));

            Logger = logger;
        }

        /// <summary>
        /// Gets the Logger.
        /// </summary>
        protected ILogger Logger { get; }
    }
}
