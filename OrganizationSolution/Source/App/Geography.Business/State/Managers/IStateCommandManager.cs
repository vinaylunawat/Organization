namespace Geography.Business.State.Manager
{
    using Geography.Business.Business.State;
    using Geography.Business.Business.State.Models;
    using Framework.Business.Manager.Command;

    public interface IStateCommandManager : ICodeCommandManager<StateErrorCode, StateCreateModel, StateUpdateModel>
    {        
    }
}
