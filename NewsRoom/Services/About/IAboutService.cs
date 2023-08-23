using NewsRoom.Areas.Admin.Models;
using NewsRoom.Models.FaqEntity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsRoom.Services.About
{
    public interface IAboutService
    {
        Task<FaqViewModel> CreateAsync(FaqCreateInputViewModel model);

        Task<FaqViewModel> EditAsync(FaqEditViewModel model);

        Task<IEnumerable<FaqViewModel>> GetAllFaqsAsync<T>();

        Task<FaqViewModel> GetByIdAsync<T>(int faqId);

        void DeleteById(int id);
        
    }
}
