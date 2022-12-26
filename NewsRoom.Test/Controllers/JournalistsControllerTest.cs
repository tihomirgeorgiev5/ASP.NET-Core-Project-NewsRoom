using MyTested.AspNetCore.Mvc;
using NewsRoom.Controllers;
using NewsRoom.Data.Models;
using NewsRoom.Models.Journalists;
using NewsRoom.Models.News;
using System.Linq;
using Xunit;

using static NewsRoom.WebConstants;

namespace NewsRoom.Test.Controllers
{
    public class JournalistsControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeForAuthorizedUsers()
             => MyController<JournalistsController>
                .Instance()
                .Calling(n => n.Become())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                   .RestrictingForAuthorizedRequests());

        [Fact]

        public void GetBecomeShouldReturnView()
            => MyController<JournalistsController>
            .Instance()
            .Calling(n => n.Become())
            .ShouldReturn()
            .View();

        [Theory]
        [InlineData("Journalist", "+359888888888")]
        public void PostBecomeShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
            string journalistName,
            string phoneNumber)
            => MyController<JournalistsController>
                  .Instance(controller => controller
                      .WithUser())
                  .Calling(n => n.Become(new BecomeJournalistFormModel
                  {
                      Name = journalistName,
                      PhoneNumber = phoneNumber
                  }))
                  .ShouldHave()
                  .ActionAttributes(attributes => attributes
                      .RestrictingForHttpMethod(HttpMethod.Post)
                      .RestrictingForAuthorizedRequests())
                  .ValidModelState()
                  .Data(data => data.WithSet<Journalist>(journalists => journalists
                      .Any(j =>
                         j.Name == journalistName &&
                         j.PhoneNumber == phoneNumber &&
                         j.UserId == TestUser.Identifier)))
                  .TempData(tempData => tempData
                      .ContainingEntryWithKey(GlobalMessageKey))
                  .AndAlso()
                  .ShouldReturn()
                  .Redirect(redirect => redirect
                      .To<NewsController>(n => n.All(With.Any<AllNewsQueryModel>())));

    }             
}
