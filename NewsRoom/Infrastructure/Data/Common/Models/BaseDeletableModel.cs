using NewsRoom.Data.Models;
using NewsRoom.Infrastructure.Data.Common.Models.Contracts;
using System;

namespace NewsRoom.Infrastructure.Data.Common.Models
{
    public abstract class BaseDeletableModel<TKey> : BaseModel<TKey>, IDeletableEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
