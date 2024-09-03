using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VetHub02.Core.Entities.identity;
using VetHub02.Core.Servises;
using VetHub02.Repository.Identity;
using VetHub02.Services;

namespace VetHub02.Extensions
{
    public static class IdentityServicesExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services,IConfiguration configuration)
        {

            Services.AddScoped<ITokenService,TokenService>();

            Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                  options.Password.RequireDigit = true;
                  options.Password.RequireLowercase = true;
                  options.Password.RequireUppercase = true;
                  options.Password.RequireNonAlphanumeric = true;
                

            }).AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();

           // Services.Configure<IdentityOptions>(opetions => opetions.SignIn.RequireConfirmedEmail = true);

           // Services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(2);

           

            Services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                }).AddJwtBearer(
                options => options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:VaildIssuer"],
                    ValidateAudience= true,
                    ValidAudience = configuration["JWT:VaildAudience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                }) ;


            return Services;
        }
    }
}
