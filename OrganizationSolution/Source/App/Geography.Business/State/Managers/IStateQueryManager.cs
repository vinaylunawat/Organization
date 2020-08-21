namespace Geography.Business.State.Manager
{
    using Framework.Business.Manager;
    using Framework.Business.Manager.Query;
    using Geography.Business.Business.State.Models;

    /// <summary>
    /// Defines the <see cref="IStateQueryManager" />.
    /// </summary>
    public interface IStateQueryManager : IManagerBase, ICodeQueryManager<StateReadModel>
    {
    }
}
