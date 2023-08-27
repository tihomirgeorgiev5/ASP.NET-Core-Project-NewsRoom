using NewsRoom.Infrastructure.Data.Common.Models.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace NewsRoom.Infrastructure.Data.Common.Models
{
    public abstract class BaseModel<TKey> : IAuditInfo
    {
        [Key]
        public TKey Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
