using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Domain
{
	public class Employees
	{
		private string _cSVPath;
		public Employees(string cSvPath)
		{
			_cSVPath = cSvPath ?? throw new ArgumentNullException(nameof(cSvPath));
		}

		private Employees(){}

		public List<Employee> GetEmployeeRecords()
		{
			var employees = CsvReader.GetEmployees(_cSVPath);

			if (employees != null)
			{
				Services services = new Services(employees);
				if (services.ValidateEmployees())
				{
					return employees;
				}
			}
			return null;
		}
		public long GetManagerBudget(string managerId)
		{
			var employees = CsvReader.GetEmployees(_cSVPath);

			if (employees != null)
			{
				Services services = new Services(employees);
				return services.GetManagersBudget(managerId);
			}
			return 0;
		}
	}
}
