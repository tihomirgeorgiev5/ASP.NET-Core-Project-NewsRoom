using System.Collections;
using System.Collections.Generic;

namespace NewsRoom.Models.Home
{
    public class IndexViewModel
    {
        public int TotalNews { get; init; }

        public int TotalReaders { get; init; }

        public int TotalWriters { get; init; }

        public List<NewsIndexViewModel> News { get; init; }
    }
}
