using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

namespace VetHub02.Extensions
{
    public static class addCros
    {

        //public static IConfiguration AddCrosOrgin(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var allowOrigins = configuration.GetValue<string>("AllowOrigins");
        //    services.AddCors(options =>
        //    {
        //        options.AddPolicy("CorsPolicy", builder =>
        //        {
        //            builder.WithOrigins(allowOrigins)
        //                .AllowAnyHeader()
        //                .AllowAnyMethod()
        //              .AllowCredentials()
        //              .AllowAnyOrigin();

        //        });
        //        options.AddPolicy("AllowHeaders", builder =>
        //        {
        //            builder.WithOrigins(allowOrigins)
        //                    .WithHeaders(HeaderNames.ContentType, HeaderNames.Server, HeaderNames.AccessControlAllowHeaders, HeaderNames.AccessControlExposeHeaders, "x-custom-header", "x-path", "x-record-in-use", HeaderNames.ContentDisposition);
        //        });
        //    });
        //    services.AddSwaggerGen(c =>
        //    {
        //        c.SwaggerDoc("v1", new OpenApiInfo { Title = "DatingApp", Version = "v1" });
        //    });
        //    return configuration;
        //}
    }
}
