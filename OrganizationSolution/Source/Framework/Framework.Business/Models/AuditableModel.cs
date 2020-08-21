namespace Framework.Business.Models
{
    using Framework.Business.Models.Models;
    using System;    

    public abstract class AuditableModel : Model, IAuditableModel
    {
        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
