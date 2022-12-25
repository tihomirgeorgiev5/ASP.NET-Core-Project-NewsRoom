using Xunit;
using MyTested.AspNetCore.Mvc;
using NewsRoom.Controllers;

using static NewsRoom.Test.Data.News;
using System.Collections.Generic;
using NewsRoom.Services.News.Models;
using FluentAssertions;

namespace NewsRoom.Test.Controllers
{
    public class HomeControllerTest
    {
        [Fact]

        public void IndexActionShouldReturnCorrectViewWithModel()
            => MyController<HomeController>
                  .Instance(instance => instance
                      .WithData(TenPublicNews))
                  .Calling(n => n.Index())
                  .ShouldReturn()
                  .View(view => view
                      .WithModelOfType<List<LatestNewsServiceModel>>()
                  .Passing(model => model.Should().HaveCount(3)));
    }
}
