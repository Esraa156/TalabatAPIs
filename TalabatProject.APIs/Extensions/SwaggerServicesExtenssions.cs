using Microsoft.AspNetCore.Builder;

namespace TalabatProject.APIs.Extensions
{
	public static class SwaggerServicesExtenssions
	{
		public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
		{
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();

			return services;
		}
		public static WebApplication UseSwaggerMiddleware(this WebApplication app)
		{
			app.UseSwagger();
			app.UseSwaggerUI();
			return app;
		}
	}
}
