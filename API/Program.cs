using Application.AppServices;
using API.Services;
using Persistence;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddGrpc();
    builder.Services.AddGrpcReflection();
    builder.Services.AddScoped<IBillingAppService, BillingAppService>();
    builder.Services.AddSingleton<IInMemoryDb, InMemoryDb>();
}

var app = builder.Build();
{
    app.MapGrpcService<GreeterService>();
    app.MapGrpcService<BillingService>();
    app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

    IWebHostEnvironment env = app.Environment;
    if (env.IsDevelopment())
    {
        app.MapGrpcReflectionService();
    }

    app.Run();
}