using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

		public async Task<List<Employee>> GetEmployeeRecords()
		{
			var employees = await _csvReader.GetEmployees(_cSVPath);
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
		public async Task<long> GetManagerBudget(string managerId)
		{
			var employees = await _csvReader.GetEmployees(_cSVPath);

			if (employees != null)
			{
				EmployeesServices services = new EmployeesServices(employees);
				return services.GetManagersBudget(managerId);
			}
			return 0;
		}
	}
}
