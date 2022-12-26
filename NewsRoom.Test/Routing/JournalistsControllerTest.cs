using Xunit;
using MyTested.AspNetCore.Mvc;
using NewsRoom.Controllers;
using NewsRoom.Models.Journalists;

namespace NewsRoom.Test.Routing
{
    public class JournalistsControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Journalists/Become")
                .To<JournalistsController>(n => n.Become());

        [Fact]
        public void PostBecomeShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request.WithPath("/Journalists/Become")
                    .WithMethod(HttpMethod.Post))
                .To<JournalistsController>(n => n.Become(With.Any<BecomeJournalistFormModel>()));
    }
}
