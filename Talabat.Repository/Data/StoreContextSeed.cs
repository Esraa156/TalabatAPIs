using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
	public static class StoreContextSeed
	{
		public async static Task SeedAsync(StoreContext _dbcontext)
		{

			if (_dbcontext.ProductBrands.Count() == 0)
			{
				var BrandData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/brands.json");
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
				if (brands?.Count() > 0)
				{
					brands = brands.Select(b => new ProductBrand()
					{

						Name = b.Name,
					}).ToList();

					foreach (var brand in brands)
					{
						_dbcontext.Set<ProductBrand>().Add(brand);

					}
					await _dbcontext.SaveChangesAsync();

				}
			}


		
		if (_dbcontext.ProductCategorys.Count() == 0)
			{
				var CategoriesData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/categories.json");
				var categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);
				if (categories?.Count() > 0)
				{
					
					foreach (var category in categories)
					{
						_dbcontext.Set<ProductCategory>().Add(category);

	}
			await _dbcontext.SaveChangesAsync();

}
			}
		}
	}
}
