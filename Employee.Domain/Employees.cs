using System;
using System.Collections.Generic;

namespace Employee.Domain
{
	public class Employees
	{
		private string _cSVPath;
		private readonly ICsvReader _csvReader;
		public Employees(string cSvPath, ICsvReader csvReader)
		{
			_cSVPath = string.IsNullOrWhiteSpace(cSvPath) ? throw new ArgumentNullException(nameof(cSvPath)) : cSvPath;
			_csvReader = csvReader;
		}

		private Employees(){}

		public List<Employee> GetEmployeeRecords()
		{
			var employees = _csvReader.GetEmployees(_cSVPath);
			if (employees != null)
			{
				EmployeesServices services = new EmployeesServices(employees);
				services.ValidateEmployees();
				if (services.IsValid)
				{
					return employees;
				}
				throw new AggregateException(services.ValidationErrors);
			}
			return null;
		}
		public long GetManagerBudget(string managerId)
		{
			var employees = _csvReader.GetEmployees(_cSVPath);

			if (employees != null)
			{
				EmployeesServices services = new EmployeesServices(employees);
				return services.GetManagersBudget(managerId);
			}
			return 0;
		}
	}
}
