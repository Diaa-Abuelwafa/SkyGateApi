using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using SkyGateApiLayer.Middlewares;
using SkyGateDomainLayer.Entities.Identity;
using SkyGateDomainLayer.Errors;
using SkyGateDomainLayer.Interfaces.Identity;
using SkyGateDomainLayer.Interfaces.UnitOfWork;
using SkyGateRepositoryLayer.Data.Contexts;
using SkyGateRepositoryLayer.Repositories;
using SkyGateServiceLayer.Services.Identity;
using SkyGateDomainLayer.Mapping_Profiles;
using SkyGateDomainLayer.Classes;
using SkyGateServiceLayer.Helpers;

namespace SkyGateApiLayer
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add CORS Configurations
            builder.Services.AddCors(Options =>
            {
                Options.AddPolicy("MyPolicy", O =>
                {
                    O.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader();
                });
            });

            // Add services to the container.

            // Add Controller Service To The DI Container And Configure Behavior Of Validation Error

            builder.Services.AddControllers().ConfigureApiBehaviorOptions((O) =>
            {
                // Configurations Of ModelState Validation
                O.InvalidModelStateResponseFactory = (Context) =>
                {

                    // Select The Errors From ModelState
                    var Errors = Context.ModelState.Where(x => x.Value.Errors.Count() > 0)
                                                   .SelectMany(x => x.Value.Errors)
                                                   .Select(x => x.ErrorMessage)
                                                   .ToList();

                    // Make The Custom Response 
                    var Response = new ApiValidationErrorResponse((int)HttpStatusCode.BadRequest, Errors);

                    return new BadRequestObjectResult(Response);
                };
            });

            // Add Identity Services To The DI Container

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<AppDbContext>()
                            .AddDefaultTokenProviders();

            // Add AppDbContext Service The DI Container
            builder.Services.AddDbContext<AppDbContext>(O =>
            {
                O.UseSqlServer(builder.Configuration.GetConnectionString("CS"));
            });

            // Configurations For Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add Custom Services To The DI Container
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IAccountService, AccountService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Configuration Of CORS Policy
            builder.Services.AddCors(O =>
            {
                O.AddPolicy("PolicySettings", O =>
                {
                    O.AllowAnyOrigin();
                    O.AllowAnyHeader();
                    O.AllowAnyMethod();
                });
            });

            // Configure Authentication Middleware To Work JWT Base
            builder.Services.AddAuthentication(O =>
            {
                // For Verify Authentication Jwt Base
                O.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                // For When The Request Come From Un Authenticated Consumer Return UnAuthorize Response
                O.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(O =>
            {
                // Check In The Token On The Expire Time
                O.SaveToken = true;

                // The Request Https Protocol
                O.RequireHttpsMetadata = true;

                // Check On Claims Matched The App Values Or Not 
                O.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Jwt:ProviderUrl"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:ConsumerUrl"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
                };
            });

            var app = builder.Build();

            // To Handle Server Error
            app.UseMiddleware<ExceptionMiddleware>();

            // Update The Database(AppDbContext)
            using (var Scope = app.Services.CreateScope())
            {
                var Context = Scope.ServiceProvider.GetRequiredService<AppDbContext>();

                try
                {
                    // Update AppDbContext Database
                    await Context.Database.MigrateAsync();

                }
                catch (Exception Ex)
                {

                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure CORS Policy
            app.UseCors("PolicySettings");

            app.UseAuthentication();

            app.UseAuthorization();

            // MiddleWare To Handle The Not Found EndPoint
            app.UseStatusCodePagesWithReExecute("/api/Errors");

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
