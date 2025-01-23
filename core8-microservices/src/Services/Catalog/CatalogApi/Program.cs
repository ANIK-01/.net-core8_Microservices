using System.Net;
using CatalogApi;
using Serilog;
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 8080);
    options.Listen(IPAddress.Any, 8081, listenOptions =>
    {
        listenOptions.UseHttps("/app/certificates/aspnetapp.pfx", "admin");
    });
});
//Add services to the container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddMarten(Opts =>
{
    Opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

//Serilog Configuration
builder.Host.SerilogConfiguration();

var app = builder.Build();
app.MapGet("/", () => "Hello from DOCKER");

//app.UseSerilogger()
app.MapCarter();
app.UseStaticFiles();
app.Run();
