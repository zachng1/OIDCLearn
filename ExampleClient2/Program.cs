using ExampleClient2.Configuration;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.ConfigureServices();

var app = builder.Build();
app.ConfigureApplication();


app.Run();