
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SkyGateDomainLayer.Entities.Identity;
using SkyGateRepositoryLayer.Data.Contexts;

namespace SkyGateApiLayer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add Services To The Dependency Injection Container

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(O =>
            {
                O.UseSqlServer(builder.Configuration.GetConnectionString("CS"));
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<AppDbContext>();

            var app = builder.Build();

            // Update-Database & SeedData
            using(var Scope = app.Services.CreateScope())
            {
                var Context = Scope.ServiceProvider.GetRequiredService<AppDbContext>();

                try
                {
                    // Update-Database
                    await Context.Database.MigrateAsync();

                    // Seed Data

                }
                catch(Exception Ex)
                {

                }
            }

            // Http Request Pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
