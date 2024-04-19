using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using TalabatProject.APIs.Errors;

namespace TalabatProject.APIs.Controllers
{

	[Route("errors/{code}")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi =true)]
	public class ErrorsController:ControllerBase
	{
		public ActionResult Error(int code)
		{
			if (code == 404)
				return NotFound(new ApiResponse(404));
			else if (code == 401)
				return Unauthorized(new ApiResponse(404));
			else
				return StatusCode(code);

		}

	}
}
