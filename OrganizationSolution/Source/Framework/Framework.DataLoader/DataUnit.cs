namespace Framework.DataLoader
{
    using System.Threading.Tasks;
    using EnsureThat;
    using Microsoft.Extensions.Logging;

    public abstract class DataUnit : IDataUnit
    {
        protected DataUnit(ILogger logger)
        {
            EnsureArg.IsNotNull(logger, nameof(logger));

            Logger = logger;
        }

        public ILogger Logger { get; private set; }

        public virtual Task LoadSeedDataAsync()
        {
            return Task.CompletedTask;
        }

        public virtual Task LoadDemoDataAsync()
        {
            return Task.CompletedTask;
        }
    }
}
