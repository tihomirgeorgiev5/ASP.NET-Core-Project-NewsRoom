using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsRoom.Infrastructure.Data.Common.Models.Contracts
{
    public interface IAuditInfo
    {
        DateTime CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
