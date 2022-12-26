using MyTested.AspNetCore.Mvc;
using NewsRoom.Controllers;
using NewsRoom.Data.Models;
using NewsRoom.Models.Journalists;
using NewsRoom.Models.News;
using System.Linq;
using Xunit;

using static NewsRoom.WebConstants;

namespace NewsRoom.Test.Pipeline

{
    public class JournalistsControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeForAuthorizedUsersAndReturnView()
            => MyPipeline
                .Configuration()
                .ShouldMap(request => request
                .WithPath("/Journalists/Become")
                .WithUser())
                .To<JournalistsController>(n => n.Become())
                .Which()
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                   .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData("Journalist", "+359888888888")]
        public void PostBecomeShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
          string journalistName,
          string phoneNumber)
          => MyPipeline
            .Configuration()
            .ShouldMap(request => request
               .WithPath("/Journalists/Become")
               .WithMethod(HttpMethod.Post)
               .WithFormFields(new 
                   {
                   Name = journalistName,
                   PhoneNumber = phoneNumber
               
               
                   })
               .WithUser()
            .WithAntiForgeryToken())
             .To<JournalistsController>(n => n.Become(new BecomeJournalistFormModel
             {
                 Name = journalistName,
                 PhoneNumber = phoneNumber
             }))
             .Which()     
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
