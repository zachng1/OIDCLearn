namespace ExampleClient.Configuration;

public static class ConfigurationExtensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddHttpClient();
    }

    public static void ConfigureApplication(this IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}