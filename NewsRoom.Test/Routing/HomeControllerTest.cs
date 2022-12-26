using Xunit;
using MyTested.AspNetCore.Mvc;
using NewsRoom.Controllers;

namespace NewsRoom.Test.Routing
{
    public class HomeControllerTest
    {
        [Fact]

        public void GetIndexRouteShouldBeMapper()
           => MyRouting
               .Configuration()
               .ShouldMap("/")
               .To<HomeController>(n => n.Index());

        [Fact]

        public void GetErrorRouteShouldBeMapper()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Error")
                .To<HomeController>(n => n.Error());
    }
}
