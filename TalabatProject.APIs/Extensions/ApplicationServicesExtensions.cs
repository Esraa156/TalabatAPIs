using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using Talabat.Service.ServiceContract.OrderService;
using TalabatProject.APIs.Errors;
using TalabatProject.APIs.Helpers;

namespace
    TalabatProject.APIs.Extensions
{
    public static class ApplicationServicesExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{

			services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
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
		public static IServiceCollection AddAuthServices(this IServiceCollection services,IConfiguration configuration) {

			services.AddAuthentication(/*JwtBearerDefaults.AuthenticationScheme*/options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(
				options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuer = true,
						ValidIssuer = configuration["JWT:ValidIsser"],
						ValidateAudience = true,
						ValidAudience = configuration["JWT:ValidAudience"],
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:AuthKey"])),
						ValidateLifetime = true,
						ClockSkew = TimeSpan.Zero
					};

				});


			services.AddScoped(typeof(IAuthService), typeof(AuthService));
			return services;





		}
	}
}
