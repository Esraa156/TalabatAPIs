using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using TalabatProject.APIs.Errors;
using TalabatProject.APIs.Helpers;

namespace 
	TalabatProject.APIs.Extensions
{
	public static class ApplicationServicesExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddAutoMapper(typeof(MappingProfiles));

			services.Configure<ApiBehaviorOptions>(options =>

			{
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var error = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
																						.SelectMany(P => P.Value.Errors)
																						.Select(E => E.ErrorMessage)
																						.ToArray();
					var response = new ApiValidationErrorResponse()
					{
						Errors = error
					};

					return new BadRequestObjectResult(response);

				};


			});

            //webApplicationBuilder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            //webApplicationBuilder.Services.AddScoped<IGenericRepository<ProductBrand>, GenericRepository<ProductBrand>>();
            //webApplicationBuilder.Services.AddScoped<IGenericRepository<ProductCategory>, GenericRepository<ProductCategory>>();

           

            return services;

		}
	}
}
