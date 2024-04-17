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
		public ProductWithBrandAndCategorySpecification():base(){

			Includes.Add(P=>P.Brand);
			Includes.Add(P=>P.Category);

			}
	}
}
