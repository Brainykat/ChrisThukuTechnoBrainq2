using System;
using System.Collections.Generic;
using System.Linq;

namespace Employee.Domain
{
	class Services
	{
		
		private List<Employee> _employees;

		public Services(List<Employee> employees)
		{
			_employees = employees ?? throw new ArgumentNullException(nameof(employees));
		}
		private Services(){}
		public bool ValidateEmployees()
		{
			if (_employees.Where(e => e.ManagerId == string.Empty).Count() > 1) throw new Exception("More than one CEO");
			var managers = _employees.Select(e => e.ManagerId);
			if (_employees.Select(e => e.Id).Except(managers).Count() > 0) throw new Exception("Some Managers not listed");
			
			foreach (var id in _employees.Select(e => e.Id).Distinct())
			{
				if (_employees.Where(i => i.Id == id).Select(m => m.ManagerId).Distinct().Count() > 1)
					throw new Exception($"Employee {id} has more than one manager");
			}
			foreach (var employee in _employees)
			{
				if (employee.Id == _employees.FirstOrDefault(e => e.Id == employee.ManagerId).Id)
					throw new Exception("Cyclic Reference detected");
			}
			return true;
		}
		public long GetManagersBudget(string managerId)
		{
			decimal total = 0;
			total += _employees.FirstOrDefault(e => e.Id == managerId).Salary;
			
			foreach (var item in _employees.Where(e => e.ManagerId == managerId))
			{
				if (isManager(item.Id))
				{
					GetManagersBudget(item.Id);
				}
				else
				{
					total += item.Salary;
				}
			}
			return Convert.ToInt64(total);
			
		}
		private bool isManager(string id) => _employees.Where(e => e.ManagerId == id).Count() > 0;
	}
}
