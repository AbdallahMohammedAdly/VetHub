using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Text.Json.Serialization;
using VetHub02.Core.Entities.identity;
using VetHub02.Core.Repositories;
using VetHub02.Errors;
using VetHub02.Extensions;
using VetHub02.Helpers;
using VetHub02.MiddleWares;
using VetHub02.Repository;
using VetHub02.Repository.Data;
using VetHub02.Repository.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace VetHub02
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // IgnoreCycles obj => obj OR circular references
            builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptionsAction: sqtOptions =>
                    {
                        sqtOptions.EnableRetryOnFailure();

                    });
            });
            builder.Services.AddAutoMapper(typeof(MappingProfiles));

           
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"),
                     sqlServerOptionsAction: sqtOptions =>
                     {
                         sqtOptions.EnableRetryOnFailure();

                     });
            });
            
            builder.Services.AddIdentityServices(builder.Configuration);//Create in IdentityExtension


            builder.Services.AddAppServices();//Create in AppExtension

            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
            builder.Services.AddTransient<IMailSettings, MailSettingsimplementation>();


            //builder.Services.AddCrosOrgin(builder.Configuration);


            
            builder.Services.AddMyError();

            var app = builder.Build();

            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            
             var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                var dbContext = services.GetRequiredService<StoreContext>();

                await dbContext.Database.MigrateAsync();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();

                await StoreContextDataSeed.SeedAsync(dbContext, userManager);

                var identityDbContext = services.GetRequiredService<AppIdentityDbContext>();
                await identityDbContext.Database.MigrateAsync();

                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>() ;
               await AppIdentityDbContextDataSeed.SeedUserAsync(userManager ,roleManager);

            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<Program>();

                logger.LogError(ex ,"an Error Occured during apply the Migration");

            }




            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseCors("devCorsPolicy");
            }

            app.UseMiddleware<ExceptionMiddleWare>();
            //# Configure CORS
            //app.add_middleware(
            //    CORSMiddleware,
            //    allow_origins = [""],
            //# Replace "" with the origins you want to allow
            //    allow_credentials = True,
            //    allow_methods = ["*"],
            //    allow_headers = ["*"],

                        app.UseHttpsRedirection();


           // app.UseStatusCodePagesWithRedirects("/Errors/{0}");

            

            app.UseAuthentication();
            app.UseAuthorization();

           

            app.MapControllers();
           

            app.Run();
        }
    }
}
