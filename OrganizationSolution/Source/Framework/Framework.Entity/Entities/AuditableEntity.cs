namespace Framework.Entity
{
    public abstract class AuditableEntity : BaseEntity
    {
        public const string CreateDateTimePropertyName = "CreateDateTime";

        public const string UpdateDateTimePropertyName = "UpdateDateTime";
    }
}
