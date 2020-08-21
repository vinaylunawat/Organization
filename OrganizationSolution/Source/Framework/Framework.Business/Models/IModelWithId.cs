namespace Framework.Business.Models
{
    public interface IModelWithId : IAuditableModel
    {
        long Id { get; set; }
    }
}
