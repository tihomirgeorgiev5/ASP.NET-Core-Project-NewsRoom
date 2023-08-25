using NewsRoom.Infrastructure.Data.Common.Models.Contracts;
using System;

namespace NewsRoom.Data.Models
{
    public abstract class BaseModel<TKey> : IAuditInfo
    {
        public TKey Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
