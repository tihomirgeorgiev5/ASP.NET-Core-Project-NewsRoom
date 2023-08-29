using Microsoft.EntityFrameworkCore;
using NewsRoom.Areas.Admin.Models;
using NewsRoom.Data;
using NewsRoom.Infrastructure.Data.Common.Models;
using NewsRoom.Infrastructure.Data.Repositories;
using NewsRoom.Models.FaqEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaqEntity = NewsRoom.Data.Models.FaqEntity;

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

            var faq1 = await _newsRoomDbContext.Faqs.AddAsync(new FaqEntity()
            {
                Answer = model.Answer,
                Question = model.Question,
                CreatedOn = DateTime.Now,
                IsDeleted = false
            });

            _newsRoomDbContext.SaveChanges();

            return new FaqViewModel();
        }

        public void DeleteById(int faqId)
        {
            var faqToDelete = _newsRoomDbContext.Faqs.FirstOrDefault(f => f.Id == faqId);

            _newsRoomDbContext.Faqs.Remove(faqToDelete);
            _newsRoomDbContext.SaveChanges();
        }

        public async Task<FaqViewModel> EditAsync(FaqEditViewModel model)
        {
            var faq = _newsRoomDbContext.Faqs.FirstOrDefault(f => f.Id == model.FaqId);

            if (faq == null)
            {
                throw new ArgumentException(string.Format(MessageConstants.Faq.FaqNotFound, model.FaqId));
            }

            
            faq.Answer = model.Answer;
            faq.Question = model.Question;
            faq.ModifiedOn = DateTime.UtcNow;

            _newsRoomDbContext.Faqs.Update(faq);
            _newsRoomDbContext.SaveChanges();

            return new FaqViewModel();
        }

        public async Task<IEnumerable<FaqViewModel>> GetAllFaqsAsync<T>()
        {
            var result = await _newsRoomDbContext.Faqs.Select(f => new FaqViewModel
            {
                Answer = f.Answer,
                FaqId = f.Id,
                Question = f.Question
            }).ToListAsync();

            return result;
        }

        public async Task<FaqViewModel> GetByIdAsync<T>(int faqId)
        {
            var faqModel = _newsRoomDbContext
                .Faqs
                .Where(f => f.Id == faqId)
                .Select(x => new FaqViewModel()
                {
                    Answer = x.Answer,
                    FaqId = x.Id,
                    Question = x.Question
                })
                .FirstOrDefault();

            return faqModel;
        }
    }
}
