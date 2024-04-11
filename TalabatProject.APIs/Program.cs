using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.APIs
{
	public class Program
	{
		//Entery Point
		public static async Task  Main(string[] args)
		{
			//StoreContext dbcontext=/*new StoreContext()*/;
			var webApplicationBuilder = WebApplication.CreateBuilder(args);

			#region Configure Service
			// Add services to the DI container.

			webApplicationBuilder.Services.AddControllers();
			//Rejister Required Web APIs services to the DI container.

			// Add services to the container.

			webApplicationBuilder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			webApplicationBuilder.Services.AddEndpointsApiExplorer();
			webApplicationBuilder.Services.AddSwaggerGen();
			webApplicationBuilder.Services.AddDbContext<StoreContext>(options=>
			{
				options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));





				});

			//webApplicationBuilder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
			//webApplicationBuilder.Services.AddScoped<IGenericRepository<ProductBrand>, GenericRepository<ProductBrand>>();
			//webApplicationBuilder.Services.AddScoped<IGenericRepository<ProductCategory>, GenericRepository<ProductCategory>>();


			webApplicationBuilder.Services.AddScoped(typeof(IGenericRepository<>), typeof(IGenericRepository<>));



			#endregion
			var app = webApplicationBuilder.Build();
			using var Scopped = app.Services.CreateScope();

			var services = Scopped.ServiceProvider;

			var _dbContext = services.GetRequiredService<StoreContext>();
			
			
			var LoggerFactory=services.GetRequiredService<ILoggerFactory>();
			try
			{
				await _dbContext.Database.MigrateAsync();//Update DataBase

				await StoreContextSeed.SeedAsync(_dbContext);//DataSeeding

			}
			catch (Exception ex)
			{
				var logger=LoggerFactory.CreateLogger<Program>();
				logger.LogError(ex,"An Error Has Been Occured during apply migration");
				Console.WriteLine(ex);

			}

			#region Configure Kestrel Middlewares
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();
			#endregion
			app.Run();


			


		}
	}
}