using System.Text.Json;
using ThunderWings.Core.Services;
using ThunderWings.Core.Services.Interfaces;
using ThunderWings.Repo.DAL;
using ThunderWings.Repo.Models;

namespace ThunderWings.Api.Helpers
{
    public static class ServiceConfigurationHelper
    {
        public static void RegisterTypes(this IServiceCollection services)
        {
            services.AddScoped<IAircraftService, AircraftService>();
            services.AddScoped<IBasketService, BasketService>();
        }

        public static void SeedAircraftData(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    if (!context.Aircraft.Any())
                    {
                        var aircraftJson = File.ReadAllText("Data/Aircraft.json");

                        var options = new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        };

                        var aircraft = JsonSerializer.Deserialize<List<Aircraft>>(aircraftJson, options);

                        context.Aircraft.AddRange(aircraft);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
