﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Repository.Data;
using TalabatProject.APIs.Errors;

namespace TalabatProject.APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BuggyController : ControllerBase
	{
		private readonly StoreContext _dbContext;

		public BuggyController(StoreContext dbContext)
		{
			_dbContext = dbContext;
		}
		[HttpGet("notfound")]
		public ActionResult GetNotFoundRequest()
		{
			var product = _dbContext.Products.Find(100);
			if(product is null)
			{
				return NotFound(new ApiResponse(404));
			}
			return Ok(product);
		}
		[HttpGet("servererror")]
		public ActionResult GetServerError()
		{
			var product = _dbContext.Products.Find(100);
			var productToReturn = product.ToString();//Will Throw Exception [Null Refernce Execptin]
			return Ok(productToReturn);
		
		}

		[HttpGet("badrequest")]
		public ActionResult GetBadRequest()
		{
			return BadRequest(new ApiResponse(400));
		}
		[HttpGet("badrequest/{id}")]
		public ActionResult GetBadRequest(int id) //ValidationError
		{
			return Ok();
		}
		[HttpGet("Unauthorized/{id}")]

		public ActionResult Unauthorized() //ValidationError
		{
			return Unauthorized(new ApiResponse(401));    

		}

	}
}
