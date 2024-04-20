using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecification
{
	public class ProductWithBrandAndCategorySpecification : BaseSpecifications<Product>
	{

		//This Constructor is used for Creating an Object .That object will be used to Get
		//AllProducts
		public ProductWithBrandAndCategorySpecification(ProductSpecParams specParams) 
			
			:base(P=>
				
				 
				 (!specParams.brandId.HasValue || P.BrandId== specParams.brandId) &&
				(!specParams.categoryId.HasValue || P.CategoryId == specParams.categoryId)
				 
				 
				 
				 
				 )
		{


			AddIncludes();
			if (!string.IsNullOrEmpty(specParams.sort))
			{
				switch (specParams.sort)
				{
					case "priceAsc":
						//OrderBy = p => p.Price;
						AddOrderBy(p => p.Price);

						break;

					case "priceDesc":
						//OrderBy = p => p.Price;
						AddOrderBy(p => p.Price);
						break;
					default:
						AddOrderBy(p => p.Name); break;

				}
			}
			else
			{
				AddOrderBy(p => p.Name);

		
			}
			ApplyPagination((specParams.pageIndex-1)*specParams.PageSize, specParams.PageSize);
		
		}

		
			

		//This Constructor is used for Creating an Object .That object will be used to Get
		//a specific product with id	
		public ProductWithBrandAndCategorySpecification(int id) : base(P=>P.Id==id)
		{

			AddIncludes();

		}

		private void AddIncludes()
		{
			Includes.Add(P => P.Brand);
			Includes.Add(P => P.Category);

		}
	}
}
