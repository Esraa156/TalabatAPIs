using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.EmployeeSpecification
{
	public class EmployeeWithDepartmentSpecification:BaseSpecifications<Employee>
	{
		public EmployeeWithDepartmentSpecification() 
			: base()
		{
			Includes.Add(E => E.department);
		}
		public EmployeeWithDepartmentSpecification(int id) 
			: base(E=>E.Id==id)
		{
			Includes.Add(E => E.department);
		}

	}
}
