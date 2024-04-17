using Talabat.Core.Entities;

namespace TalabatProject.APIs.DTOs
{
	public class ProductToReturnDto
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string PictureUrl { get; set; }
		public decimal Price { get; set; }

		public int BrandId { get; set; }    // Foreign Key Column => ProductBrand
		public string Brand { get; set; }//Nav Property

		public int CategoryId { get; set; }    // Foreign Key Column => ProductCategory

		public string Category { get; set; }//Nav Property

	}
}
