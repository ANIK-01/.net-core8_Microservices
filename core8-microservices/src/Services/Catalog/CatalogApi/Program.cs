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
builder.Host.SerilogConfiguration();
var app = builder.Build();
app.MapGet("/", () => "Hello from DOCKER");
//Configure the HTTp request pipeline
//app.UseSerilogger()
app.MapCarter();

app.Run();
