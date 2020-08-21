namespace Geography.Business.State.Manager
{
    using Framework.Business.Manager.Command;
    using Geography.Business.Business.State;
    using Geography.Business.Business.State.Models;

    /// <summary>
    /// Defines the <see cref="IStateCommandManager" />.
    /// </summary>
    public interface IStateCommandManager : ICodeCommandManager<StateErrorCode, StateCreateModel, StateUpdateModel>
    {
    }
}
