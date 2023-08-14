using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsRoom.Areas.Admin.Models
{
    public class FaqCreateInputViewModel
    {
        public string Question { get; set; }

        public string Answer { get; set; }
    }
}
