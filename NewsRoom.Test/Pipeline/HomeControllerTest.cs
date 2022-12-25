using FluentAssertions;
using MyTested.AspNetCore.Mvc;
using NewsRoom.Controllers;
using NewsRoom.Services.News.Models;
using System.Collections.Generic;
using Xunit;

using static NewsRoom.Test.Data.News;

namespace NewsRoom.Test.Pipeline


{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
        {
            MyMvc
                .Pipeline()
                .ShouldMap("/")
                .To<HomeController>(n => n.Index())
                .Which(controller => controller
                      .WithData(TenPublicNews))
                /* .ShouldHave()
                 .MemoryCache(cache => cache
                 .ContainingEntryWithKey(LatestNewsCacheKey))
                 .AndAlso() */
                .ShouldReturn()
                .View(view => view
                      .WithModelOfType<List<LatestNewsServiceModel>>()
                      .Passing(m => m.Should().HaveCount(3)));

        }

        [Fact]

        public void ErrorShouldReturnView()
            => MyMvc
                .Pipeline()
                .ShouldMap("/Home/Error")
                .To<HomeController>(n => n.Error())
                .Which()
                .ShouldReturn()
                .View();

    

    }
}
