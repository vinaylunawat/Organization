namespace Geography.Business.State.Manager
{
    using Geography.Business.Business.State.Models;
    using Framework.Business.Manager;
    using Framework.Business.Manager.Query;

    public interface IStateQueryManager : IManagerBase, ICodeQueryManager<StateReadModel>
    {
    }
}
