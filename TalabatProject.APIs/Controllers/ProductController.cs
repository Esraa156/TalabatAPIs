using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace TalabatProject.APIs.Controllers
{

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

			var Products = await genericRep.GetAllAsync();
			//OkObjectResult result=new OkObjectResult(Products);
			return Ok(Products);

		}
	}
}