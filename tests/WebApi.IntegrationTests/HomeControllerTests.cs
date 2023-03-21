using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace WebApi.IntegrationTests;

public class HomeControllerTests : IClassFixture<WebApiFactory>
{
    private readonly WebApiFactory _apiFactory;

    public HomeControllerTests(WebApiFactory apiFactory)
    {
        _apiFactory = apiFactory;
    }

    [Fact]
    public async Task Get_ReturnsOk()
    {
        var client = _apiFactory.CreateClient();

        var response = await client.GetAsync("/?name=Name");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();

        content.Should().Contain("Hello");
    }
}
