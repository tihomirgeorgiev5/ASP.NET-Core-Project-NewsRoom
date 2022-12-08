using NewsRoom.Services.News.Models;
using System.Collections.Generic;

namespace NewsRoom.Models.Home
{
    public class IndexViewModel
    {
        public int TotalNews { get; init; }

        public int TotalReaders { get; init; }

        public int TotalWriters { get; init; }

        public List<LatestNewsServiceModel> News { get; init; }
    }
}
