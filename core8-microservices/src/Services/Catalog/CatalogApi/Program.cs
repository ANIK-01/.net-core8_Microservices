using System.Net;
using CatalogApi;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

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
builder.WebHost.ConfigureKestrel(options =>
{
    var certPath = Path.Combine(Directory.GetCurrentDirectory(), "certificates", "aspnetapp.pfx");
    var certPassword = "admin";
    Console.WriteLine($"Certpath, {certPath}");
    Console.WriteLine($"Certpass, {certPassword}");
    options.Listen(IPAddress.Any, 8080);
    options.Listen(IPAddress.Any, 8081, listenOptions =>
    {
        Console.WriteLine(listenOptions.UseHttps(certPath, certPassword));
    });
});



var app = builder.Build();
app.MapGet("/", () => "Hello from DOCKER");
//Configure the HTTp request pipeline
//app.UseSerilogger()
app.MapCarter();
app.UseStaticFiles();
app.Run();
