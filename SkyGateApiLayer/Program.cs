
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkyGateApiLayer.Middlewares;
using SkyGateDomainLayer.Entities.Identity;
using SkyGateDomainLayer.Errors;
using SkyGateRepositoryLayer.Data.Contexts;
using System.Net;

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

            // Configure The Behavior Of The Api Filter For The Validation 
            builder.Services.Configure<ApiBehaviorOptions>(O =>
            {
                O.InvalidModelStateResponseFactory = O =>
                {
                    var Errors = O.ModelState.Where(x => x.Value.Errors.Count() > 0)
                                .SelectMany(x => x.Value.Errors)
                                .Select(x => x.ErrorMessage).ToList();

                    var Response = new ApiValidationErrorResponse((int)HttpStatusCode.BadRequest, Errors);

                    return new BadRequestObjectResult(Response);
                };
            });

            builder.Services.AddDbContext<AppDbContext>(O =>
            {
                O.UseSqlServer(builder.Configuration.GetConnectionString("CS"));
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<AppDbContext>();

            var app = builder.Build();

            // Handling Exception Middleware
            app.UseMiddleware<ExceptionMiddleware>();

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

            app.UseStatusCodePagesWithReExecute("/api/Error");

            app.MapControllers();

            app.Run();
        }
    }
}
