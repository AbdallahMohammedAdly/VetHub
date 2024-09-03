using System.Net;
using System.Text.Json;
using VetHub02.Errors;

namespace VetHub02.MiddleWares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleWare> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleWare(RequestDelegate next ,ILogger<ExceptionMiddleWare> logger,IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
         
        }
        public async Task InvokeAsync(HttpContext httpContext) 
        {
            try
            {
                await next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var res = env.IsDevelopment() ? new ApiExceptionError((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString())
                    : new ApiExceptionError((int)HttpStatusCode.InternalServerError , ex.Message);

                var result = env.IsProduction() ? new ApiExceptionError((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString())
                  : new ApiExceptionError((int)HttpStatusCode.InternalServerError, ex.Message);

                var text = JsonSerializer.Serialize(result, options);
                var json = JsonSerializer.Serialize(res,options);

                await httpContext.Response.WriteAsync(text);
                await httpContext.Response.WriteAsync(json);

            }
        }

       
    }
}
