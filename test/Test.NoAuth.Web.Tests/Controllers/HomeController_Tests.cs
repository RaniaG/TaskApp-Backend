using System.Threading.Tasks;
using Test.NoAuth.Web.Controllers;
using Shouldly;
using Xunit;

namespace Test.NoAuth.Web.Tests.Controllers
{
    public class HomeController_Tests: NoAuthWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}
