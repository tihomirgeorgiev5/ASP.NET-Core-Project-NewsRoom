using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsRoom.Infrastructure.Data.Common.Models.Contracts
{
    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}
