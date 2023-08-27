using Microsoft.EntityFrameworkCore;
using NewsRoom.Areas.Admin.Models;
using NewsRoom.Data;
using NewsRoom.Data.Models;
using NewsRoom.Infrastructure.Data.Repositories;
using NewsRoom.Infrastructure.Data;
using NewsRoom.Infrastructure.Data.Common;
using NewsRoom.Infrastructure.Data.Common.Models;
using NewsRoom.Models.FaqEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaqEntity = NewsRoom.Infrastructure.Data.Common.Models.FaqEntity;

namespace NewsRoom.Services.About
{
    public class AboutService : IAboutService
    {
        private readonly IAppRepository _faqRepo;
        private readonly NewsRoomDbContext _newsRoomDbContext;
        public AboutService(IAppRepository faqRepo, NewsRoomDbContext _newsRoomDbContext)
        {
            this._faqRepo = faqRepo;
            this._newsRoomDbContext = _newsRoomDbContext;
        }

        public async Task<FaqViewModel> CreateAsync(FaqCreateInputViewModel model)
        {
            var faq = new FaqViewModel
            {
                Question = model.Question,
                Answer = model.Answer,
            };

            bool isExist = await _newsRoomDbContext.Faqs
                .Where(f => f.Question == model.Question && f.Answer == model.Answer)
                .AnyAsync();

            if (isExist)
            {
                throw new ArgumentException(string.Format(MessageConstants.Faq.FaqAlreadyExist, model.Question, model.Answer));
            }

           // var faq1 = await _newsRoomDbContext.Faqs.AddAsync(new FaqEntity()
           // {
           //     Answer = model.Answer,
           //     Question = model.Question,
           //     CreatedOn = DateTime.Now,
           //     IsDeleted = false
           // });

            _newsRoomDbContext.SaveChanges();

            return new FaqViewModel();
        }

        public void DeleteById(int faqId)
        {
            var faqToDelete = _newsRoomDbContext.Faqs.FirstOrDefault(f => f.Id == faqId);

            _newsRoomDbContext.Faqs.Remove(faqToDelete);
            _newsRoomDbContext.SaveChanges();
        }

        public Task<FaqViewModel> EditAsync(FaqEditViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FaqViewModel>> GetAllFaqsAsync<T>(int faqId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FaqViewModel>> GetAllFaqsAsync<T>()
        {
            throw new NotImplementedException();
        }

        public Task<FaqViewModel> GetByIdAsync<T>(int faqId)
        {
            throw new NotImplementedException();
        }
    }
}
