using NewsRoom.Areas.Admin.Models;
using NewsRoom.Models.FaqEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsRoom.Services.About
{
    public class AboutService : IAboutService
    {
        public Task<FaqViewModel> CreateAsync(FaqCreateInputViewModel model)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<FaqViewModel> EditAsync(FaqEditViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FaqViewModel>> GetAllFaqsAsync<T>(int faqId)
        {
            throw new NotImplementedException();
        }
    }
}
