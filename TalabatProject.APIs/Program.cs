using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Net;
using System.Text;
using System.Text.Json;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories.Contract;
using Talabat.Service.ServiceContract;
using Talabat.Repository;
using Talabat.Repository._Identity;
using Talabat.Repository.Data;
using TalabatProject.APIs.Errors;
using TalabatProject.APIs.Extensions;
using TalabatProject.APIs.Helpers;
using TalabatProject.APIs.Middlewares;

namespace Talabat.APIs
{
    public class Program
	{
		//Entery Point
		public static async Task Main(string[] args)
		{
			//StoreContext dbcontext=/*new StoreContext()*/;
			var webApplicationBuilder = WebApplication.CreateBuilder(args);

			#region Configure Service
			// Add services to the DI container.
			webApplicationBuilder.Services.AddControllers().AddNewtonsoftJson(options=>
			{
				options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
			});
			//Rejister Required Web APIs services to the DI container.

			// Add services to the container.

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			
			webApplicationBuilder.Services.AddSwaggerServices();
			webApplicationBuilder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));





			});
			webApplicationBuilder.Services.AddDbContext<ApplicationIdentityDbContext>(options
				=>
			{
				options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("IdentityConnection"));
			});

			webApplicationBuilder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connection = webApplicationBuilder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            }
            );
            webApplicationBuilder.Services.AddApplicationServices();
			webApplicationBuilder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{

			})
				.AddEntityFrameworkStores<ApplicationIdentityDbContext>();
			webApplicationBuilder.Services.AddAuthServices(webApplicationBuilder.Configuration);


			#endregion
			var app = webApplicationBuilder.Build();
			using var Scopped = app.Services.CreateScope();

			var services = Scopped.ServiceProvider;

			var _dbContext = services.GetRequiredService<StoreContext>();

			var _IdentitydbContext = services.GetRequiredService<ApplicationIdentityDbContext>();

			var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
			var logger = LoggerFactory.CreateLogger<Program>();

			try
			{
				await _dbContext.Database.MigrateAsync();//Update DataBase
				await _IdentitydbContext.Database.MigrateAsync();//Update DataBase

				var _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
				await ApplicationIdentityDbContextSeed.SeedUsersAsync(_userManager);
				await StoreContextSeed.SeedAsync(_dbContext);//DataSeeding

			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An Error Has Been Occured during apply migration");
				Console.WriteLine(ex);

			}

			#region Configure Kestrel Middlewares
			//	app.UseMiddleware<ExceptionMiddleware>();
			app.Use(async (httpContext, _next) =>
			{
				try
				{
					//take an action with the request
					await _next.Invoke(httpContext); // go to next middleware
													 //take an action with the response
				}
				catch (Exception ex)
				{
					logger.LogError(ex.Message); // development environment
					httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					httpContext.Response.ContentType = "application/json";
					var response = app.Environment.IsDevelopment() ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) :
					new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
					var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
					var json = JsonSerializer.Serialize(response, options);
					await httpContext.Response.WriteAsync(json);
				}


			}




			);

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwaggerMiddleware();

			}

			app.UseStatusCodePagesWithReExecute("/errors/{0}");
			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();
			app.UseStaticFiles();
			#endregion
			app.Run();





		}
	}
}