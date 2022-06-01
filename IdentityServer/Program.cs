using IdentityServer.Configuration;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.ConfigureServices();
var app = builder.Build();
app.ConfigureApp();
app.Run();