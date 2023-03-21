using System.Threading;
using GrpcService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using WebApi.IntegrationTests.Helpers;

namespace WebApi.IntegrationTests;

public class WebApiFactory : WebApplicationFactory<IApiMarker>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var mockCall = CallHelpers.CreateAsyncUnaryCall(new HelloReply
        {
            Message = "Hello you!"
        });
        var mockClient = new Mock<Greeter.GreeterClient>();
        mockClient
            .Setup(m => m.SayHelloAsync(
                It.IsAny<HelloRequest>(), null, null, CancellationToken.None))
            .Returns(mockCall);
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<Greeter.GreeterClient>();
            services.AddSingleton<Greeter.GreeterClient>(mockClient.Object);
        });
        base.ConfigureWebHost(builder);
    }
}