﻿using AutoMapper;
using NewsRoom.Data.Models;
using NewsRoom.Models.Home;
using NewsRoom.Models.News;
using NewsRoom.Services.News.Models;

namespace NewsRoom.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<ANews, NewsIndexViewModel>();
            this.CreateMap<NewsDetailsServiceModel, NewsFormModel>();

            this.CreateMap<ANews, NewsDetailsServiceModel>()
                .ForMember(n => n.UserId, cfg => cfg.MapFrom(n => n.Journalist.UserId));
        }
    }
}