using System;
using System.Collections.Generic;
using System.Linq;

namespace Employee.Domain
{
	public class Services
	{

		private List<Employee> _employees;

		public Services(List<Employee> employees)
		{
			_employees = employees ?? throw new ArgumentNullException(nameof(employees));
		}
		private Services() { }
		public bool ValidateEmployees()
		{
			if (_employees.Where(e => e.ManagerId == string.Empty || e.ManagerId == null).Count() > 1) throw new Exception("More than one CEO");
			var managers = _employees.Where(r=>r.ManagerId != null && r.ManagerId != string.Empty).Select(e => e.ManagerId);
			foreach (var manager in managers)
			{
				if(_employees.FirstOrDefault(e => e.Id == manager) == null) throw new Exception("Some Managers not listed");
			}
			foreach (var id in _employees.Select(e => e.Id).Distinct())
			{
				if (_employees.Where(i => i.Id == id).Select(m => m.ManagerId).Distinct().Count() > 1)
					throw new Exception($"Employee {id} has more than one manager");
			}
			foreach (var employee in _employees.Where(e => e.ManagerId != string.Empty && e.ManagerId != null))
			{
				var id = _employees.Where(e => e.ManagerId != string.Empty && e.ManagerId != null)
					.FirstOrDefault(e => e.Id == employee.ManagerId);
				if(id != null)
				if (id.ManagerId == employee.Id)
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
					 total += GetManagersBudget(item.Id);
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
