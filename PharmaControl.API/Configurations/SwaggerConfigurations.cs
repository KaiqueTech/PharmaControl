using Microsoft.OpenApi.Models;

namespace PharmaControl.API.Configurations;

public static class SwaggerConfigurations
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "PharmaControl System API",
                Version = "v1",
                Description = "API to control validity of products",
                Contact = new OpenApiContact
                {
                    Name = "Kaique Bezerra",
                    Email = "developerkaique99@gmail.com",
                    Url = new Uri("https://github.com/KaiqueTech")
                }
            });
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "PharmaControl API v1");
            c.RoutePrefix = string.Empty;
        });

        return app;
    }
}