namespace Framework.Business.Models
{
    using Framework.Business.Models.Models;
    using System;

    /// <summary>
    /// Defines the <see cref="AuditableModel" />.
    /// </summary>
    public abstract class AuditableModel : Model, IAuditableModel
    {
        /// <summary>
        /// Gets or sets the CreatedDate.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the ModifiedDate.
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
