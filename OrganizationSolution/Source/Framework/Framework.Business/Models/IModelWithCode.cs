namespace Framework.Business.Models
{
    public interface IModelWithCode : IModel
    {
        string Code { get; set; }
    }
}
