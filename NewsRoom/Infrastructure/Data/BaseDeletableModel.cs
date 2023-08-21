using System;

namespace NewsRoom.Infrastructure.Data
{
    public abstract class BaseDeletableModel<TKey>
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
