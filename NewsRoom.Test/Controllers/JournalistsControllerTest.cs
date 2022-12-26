using MyTested.AspNetCore.Mvc;
using NewsRoom.Controllers;
using Xunit;

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

    }
}
