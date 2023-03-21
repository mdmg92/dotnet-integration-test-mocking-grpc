using GrpcService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddGrpcClient<Greeter.GreeterClient>(options =>
{
    options.Address = new Uri(builder.Configuration.GetConnectionString("GrpcService"));
});

var app = builder.Build();

app.MapControllers();

app.Run();