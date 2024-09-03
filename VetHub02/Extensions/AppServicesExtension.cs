using Microsoft.AspNetCore.Mvc;
using VetHub02.Core.Repositories;
using VetHub02.Repository.Data;
using VetHub02.Repository;
using VetHub02.Errors;
using VetHub02.Core.UnitOfWork;
using VetHub02.Core.Entities.identity;
using Microsoft.AspNetCore.Identity;

namespace VetHub02.Extensions
{
    public static class AppServicesExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection Services) 
        {

          
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            Services.AddScoped<IUnitOfWork, UnitOfWork>();

            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (ActionContext) => 
                {
                    
                    {
                        var errors = ActionContext.ModelState.Where(p => p.Value?.Errors.Count() > 0)
                          .SelectMany(p => p.Value?.Errors!).Select(E => E.ErrorMessage).ToArray();
                        var validation = new ApivalidationError()
                        {
                            Errors = errors
                        };
                        return new BadRequestObjectResult(validation);

                    }


                  
                    
                };
            });


            return Services; 
        }
    }
}
