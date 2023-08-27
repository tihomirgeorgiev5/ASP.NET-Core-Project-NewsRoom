using NewsRoom.Infrastructure.Data.Common.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsRoom.Infrastructure.Data.Common.Models
{
    public abstract class BaseModel<TKey> : IAuditInfo
    {
        public TKey Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
