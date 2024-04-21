using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecification
{
	public class ProductWithFilterationForCountSpecefications : BaseSpecifications<Product>

	{
		public ProductWithFilterationForCountSpecefications(ProductSpecParams specParams) : base(P =>
			 (!specParams.brandId.HasValue || P.BrandId == specParams.brandId.Value) &&
			 (!specParams.categoryId.HasValue || P.CategoryId == specParams.categoryId.Value)
		)
		{
		
		}
	}
}
