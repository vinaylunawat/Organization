namespace Framework.Business.Manager
{
    using EnsureThat;    
    using Microsoft.Extensions.Logging;    

    public abstract class ManagerBase : IManagerBase
    {
        protected ManagerBase(ILogger logger)
        {
            EnsureArg.IsNotNull(logger, nameof(logger));

            Logger = logger;
        }

        protected ILogger Logger { get; }
    }
}
