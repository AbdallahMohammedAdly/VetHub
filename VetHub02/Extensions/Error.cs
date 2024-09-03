namespace VetHub02.Extensions
{
    public static class Error
    {

        public static IServiceCollection AddMyError(this IServiceCollection services)
        {
           services.AddCors(options =>
            {
                options.AddPolicy("devCorsPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:800").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
                    builder.SetIsOriginAllowed(origin => true);
                });
                //options.AddPolicy("devCorsPolicy", builder =>
                //{
                //    var allowedOrigins = new[]
                //            {
                //                "https://abdallahmeaad.github.io",
                //                "https://example.com",
                //                "https://anotherexample.com"
                //            };

                //    builder.AllowAnyMethod()
                //           .AllowAnyHeader()
                //           .SetIsOriginAllowed(origin =>
                //           {
                //               var uri = new Uri(origin);
                //               return uri.Host == "localhost" || allowedOrigins.Contains(origin);
                //           });
                //});
                //options.AddPolicy("devCorsPolicy", builder =>
                //{
                //    builder.AllowAnyOrigin()
                //           .AllowAnyMethod()
                //           .AllowAnyHeader();
                //});

                //options.AddPolicy("devCorsPolicy", builder =>
                //{
                //    builder.WithOrigins("https://abdallahmeaad.github.io/VetHub-Api/")
                //           .AllowAnyMethod()
                //           .AllowAnyHeader();

                //    builder.SetIsOriginAllowed(origin =>
                //    {
                //        var uri = new Uri(origin);
                //        return uri.Host == "localhost" || uri.Host == "https://abdallahmeaad.github.io/VetHub-Api/";
                //    });
                //});

            });

            return services;
        }
    }
}
