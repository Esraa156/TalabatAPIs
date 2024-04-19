using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.ProductSpecification;
using TalabatProject.APIs.DTOs;
using TalabatProject.APIs.Errors;

namespace TalabatProject.APIs.Controllers
{

	[ApiController]

	[Route("api/[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly IGenericRepository<Product> genericRep;
		private readonly IMapper _mapper;

		public ProductController(IGenericRepository<Product> genericRep,IMapper mapper)
		{
			this.genericRep = genericRep;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
		{
			var spec = new ProductWithBrandAndCategorySpecification();

			var Products = await genericRep.GetAllAsyncSpec(spec);
			//OkObjectResult result=new OkObjectResult(Products);
			return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(Products));

		 }
		[ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecification(id);

			var product = await genericRep.GetAsyncSpec(spec);
			if (product is null)
				return NotFound(new ApiResponse(404) ); //404
			return Ok(_mapper.Map<Product,ProductToReturnDto>(product)); //200
		}
	}
}