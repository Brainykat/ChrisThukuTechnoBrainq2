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
			if (string.IsNullOrWhiteSpace(cSvPath)) throw new ArgumentNullException(nameof(cSvPath));
			_cSVPath = cSvPath; //?? throw new ArgumentNullException(nameof(cSvPath));
			_csvReader = csvReader;
		}

		private Employees(){}

		public List<Employee> GetEmployeeRecords()
		{
			var employees = _csvReader.GetEmployees(_cSVPath);

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
			var employees = _csvReader.GetEmployees(_cSVPath);

			if (employees != null)
			{
				Services services = new Services(employees);
				return services.GetManagersBudget(managerId);
			}
			return 0;
		}
	}
}
