using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.EmployeeSpecification;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications.ProductSpecification;
using TalabatProject.APIs.Errors;

namespace TalabatProject.APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly IGenericRepository<Employee> _employeerepo;

		public EmployeeController(IGenericRepository<Employee> employeerepo)
		{
			_employeerepo = employeerepo;
		}
		[HttpGet] //GET  /api/Employees

		public async Task<ActionResult<IEnumerable<Employee> >>GetEmployees() 
		{
			
			var spec =new EmployeeWithDepartmentSpecification();
			var employees = await _employeerepo.GetAllAsyncSpec(spec);
			return Ok(employees);

		}
		[HttpGet("{id}")]
		public async Task<ActionResult<Employee>> GetEmployee(int id)
		{
			var spec = new EmployeeWithDepartmentSpecification(id);

			var employee = await _employeerepo.GetAsyncSpec(spec);

			if (employee is null)
				return NotFound(new ApiResponse(404));
			return Ok(employee); //200
		}
	}
}
