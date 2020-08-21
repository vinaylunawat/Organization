namespace Framework.Entity
{
    /// <summary>
    /// Defines the <see cref="AuditableEntity" />.
    /// </summary>
    public abstract class AuditableEntity : BaseEntity
    {
        /// <summary>
        /// Defines the CreateDateTimePropertyName.
        /// </summary>
        public const string CreateDateTimePropertyName = "CreateDateTime";

        /// <summary>
        /// Defines the UpdateDateTimePropertyName.
        /// </summary>
        public const string UpdateDateTimePropertyName = "UpdateDateTime";
    }
}
