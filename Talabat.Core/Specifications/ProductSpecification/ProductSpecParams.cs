using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications.ProductSpecification
{
	public class ProductSpecParams
	{


		private const int MaxPageSize = 10; 
		private int pageSize {  get; set; }	
		public int pageIndex { get; set; } = 1;

		public string? sort { get; set; }
		public int? brandId { get; set; }
		public int? categoryId { get; set; }
		public int PageSize
		{
			get { return pageSize;}
			set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
		}
		

	}
}
