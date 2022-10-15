using NewsRoom.Models;
using System.Collections.Generic;

namespace NewsRoom.Services.News
{
   public interface INewsService
    {
        NewsQueryServiceModel All(
            string area,
            string searchTerm,
            NewsSorting newsSorting,
            int currentPage,
            int newsPerPage
            );

        IEnumerable<string> AllNewsAreas();
    }
}
