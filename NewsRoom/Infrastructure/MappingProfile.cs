using AutoMapper;
using NewsRoom.Data.Models;
using NewsRoom.Models.News;
using NewsRoom.Services.News.Models;

namespace NewsRoom.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Category, NewsCategoryServiceModel>();

            this.CreateMap<News, LatestNewsServiceModel>();
            this.CreateMap<NewsDetailsServiceModel, NewsFormModel>();

            this.CreateMap<News, NewsServiceModel>()
                .ForMember(n => n.CategoryName, cfg => cfg.MapFrom(n => n.Category.Name));

            this.CreateMap<News, NewsDetailsServiceModel>()
                .ForMember(n => n.UserId, cfg => cfg.MapFrom(n => n.Journalist.UserId))
                .ForMember(n => n.CategoryName, cfg => cfg.MapFrom(n => n.Category.Name));
        }
    }
}
