using Microsoft.IdentityModel.Tokens;

namespace ExampleApi.Configuration;

public static class ConfigurationExtensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:7131"; //TODO: get from config
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });
    }

    public static void ConfigureApp(this IApplicationBuilder app)
    {
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}