using AutoMapper;
using NewsRoom.Models.News;
using NewsRoom.Services.News.Models;

namespace NewsRoom.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<NewsDetailsServiceModel, NewsFormModel>();
        }
    }
}
