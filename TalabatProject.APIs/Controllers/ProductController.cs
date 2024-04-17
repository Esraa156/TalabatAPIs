using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.ProductSpecification;

namespace TalabatProject.APIs.Controllers
{

	[ApiController]

	[Route("api/[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly IGenericRepository<Product> genericRep;

		public ProductController(IGenericRepository<Product> genericRep)
		{
			this.genericRep = genericRep;
		}
		[HttpGet]
		public async Task<IActionResult> GetProducts()
		{
			var spec = new ProductWithBrandAndCategorySpecification();

			var Products = await genericRep.GetAllAsyncSpec(spec);
			//OkObjectResult result=new OkObjectResult(Products);
			return Ok(Products);

		 }

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var product = await genericRep.GetAsync(id);
			if (product is null)
				return NotFound(new { Message = "Not Found", StatusCode = 404 }); //404
			return Ok(product); //200
		}
	}
}