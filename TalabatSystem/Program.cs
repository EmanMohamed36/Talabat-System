
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PersistenceLayer;
using PersistenceLayer.Data;
using PersistenceLayer.Identity;
using PersistenceLayer.Repositories;
using ServiceAbstractionLayer;
using ServiceLayer;
using StackExchange.Redis;
using System.Text;
using TalabatSystem.CustomMiddleWares;
using TalabatSystem.Factories;

namespace TalabatSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddAutoMapper((action) => { }, typeof(ServiceLayerAssemblyReference).Assembly);
            //builder.Services.AddAutoMapper((action) => { }, typeof(ProductProfile));
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddSingleton<IConnectionMultiplexer>((_) => {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RdisConnection"));
            
            });


            #region Identity
            builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
                });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<StoreIdentityDbContext>();

            //builder.Services.AddIdentityCore<ApplicationUser>().AddRoles<IdentityRole>()
            //       .AddEntityFrameworkStores<StoreIdentityDbContext>();


            #endregion


            #endregion

            #region enable authentication ans jwt
            // Enable Authentication + JWT
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["JWTOptions:Issuer"],
                    ValidAudience = builder.Configuration["JWTOptions:Audiance"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["JWTOptions:SecretKey"]))
                };
            });
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = ctx =>
                {
                    ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
            });

            #endregion

            var app = builder.Build();

            #region Call Seeding service Mannually

            using var scope = app.Services.CreateScope();
            var seedObj = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await seedObj.DataSeedAsync();
            await seedObj.IdentityDataSeedAsync();
            #endregion


            #region Configure the HTTP request pipeline.

            //app.Use(async (RequestContent, NextMiddleWare) =>
            //{
            //    Console.WriteLine("Request Under Processing");
            //    await NextMiddleWare.Invoke();
            //    Console.WriteLine("Waiting response");
            //});

           
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //builder.Services.Configure<ApiBehaviorOptions>((options) =>
            //{
            //    options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorResponse;
            //});

            app.UseStaticFiles(); //For images ,files

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}

//how serviceLayerAssemblyReference see product service?
//profile ????? ??? ?? ???? ????? ??



