using MyTested.AspNetCore.Mvc;
using NewsRoom.Controllers;
using Xunit;

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




    }
}
