using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace PlatformService.Data {
    public static class PrepDb {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd) {
            using(var serviceScope = app.ApplicationServices.CreateScope()) {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd) 
        { 
            Console.WriteLine("----> SEEDING DATA.");
            if (isProd) {
                Console.WriteLine("----> Running migrations.");

                try {
                    context.Database.Migrate();
                }catch(Exception ex) {
                    Console.WriteLine($"Could not run migration {ex.Message}");
                }
            }

            if (!context.PlatformModels.Any()) {
                Console.WriteLine("----> No data found, adding some data.");
                context.PlatformModels.AddRange(
                    new Models.PlatformModel() {
                        Name = "Dotnet",
                        Publisher = "Microsoft",
                        Cost = "Free",
                    },
                    new Models.PlatformModel() {
                        Name = "Java",
                        Publisher = "Java",
                        Cost = "Free",
                    },
                    new Models.PlatformModel() {
                        Name = "Sql",
                        Publisher = "Microsoft",
                        Cost = "Free",
                    }
                );
            }else {
                Console.WriteLine("----> We already have data, so no data was added.");
            }
        }
    }
}