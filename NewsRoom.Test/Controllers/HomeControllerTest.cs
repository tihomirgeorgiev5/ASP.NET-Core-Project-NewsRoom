using FluentAssertions;
using MyTested.AspNetCore.Mvc;
using NewsRoom.Controllers;
using NewsRoom.Services.News.Models;
using System;
using System.Collections.Generic;
using Xunit;

using static NewsRoom.Test.Data.News;
using static NewsRoom.WebConstants.Cache;

namespace NewsRoom.Test.Controllers
{
    public class HomeControllerTest
    {
        [Fact]

        public void GetIndexActionShouldReturnCorrectViewWithModel()
            => MyController<HomeController>
                  .Instance(controller => controller
                       .WithData(TenPublicNews))
                  .Calling(n => n.Index())
                  .ShouldHave()
                  .MemoryCache(cache => cache
                       .ContainingEntry(entry => entry
                         .WithKey(LatestNewsCacheKey)
                         .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(15))
                         .WithValueOfType<List<LatestNewsServiceModel>>()))
                   .AndAlso()
                   .ShouldReturn()
                   .View(view => view
                         .WithModelOfType<List<LatestNewsServiceModel>>()
                         .Passing(model => model.Should().HaveCount(3)));

        [Fact]

        public void GetErrorShouldReturnView()
            => MyController<HomeController>
                   .Instance()
                   .Calling(n => n.Error())
                   .ShouldReturn()
                   .View();

    }
}
