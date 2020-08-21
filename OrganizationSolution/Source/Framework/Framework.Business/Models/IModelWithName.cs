namespace Framework.Business.Models
{
    public interface IModelWithName : IModel
    {
        string Name { get; set; }
    }
}
