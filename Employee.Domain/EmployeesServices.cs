using System;
using System.Collections.Generic;
using System.Linq;

namespace Employee.Domain
{
	public class EmployeesServices
	{

		private List<Employee> _employees;
		public bool IsValid { get; private set; } = true;
		public List<Exception> ValidationErrors { get; private set; } = new List<Exception>();

		public EmployeesServices(List<Employee> employees)
		{
			_employees = employees ?? throw new ArgumentNullException(nameof(employees));
		}
		private EmployeesServices() { }
		public void ValidateEmployees()
		{
			CheckNumberOfCEOs();
			CheckEmployeeWithMoreThanOneManger();
			CheckAllManagersAreListed();
			CheckCyclicReference();
		}
		private void CheckNumberOfCEOs()
		{
			if (_employees.Where(e => e.ManagerId == string.Empty || e.ManagerId == null).Count() > 1)
			{
				IsValid = false;
				ValidationErrors.Add(new Exception("More than one CEO listed"));
			}
		}
		private void CheckAllManagersAreListed()
		{
			var managers = _employees.Where(r => r.ManagerId != null && r.ManagerId != string.Empty).Select(e => e.ManagerId);
			foreach (var manager in managers)
			{
				if (_employees.FirstOrDefault(e => e.Id == manager) == null)
				{
					IsValid = false;
					ValidationErrors.Add(new Exception("Some Managers not listed"));
				}
			}
		}
		private void CheckEmployeeWithMoreThanOneManger()
		{
			foreach (var id in _employees.Select(e => e.Id).Distinct())
			{
				if (_employees.Where(i => i.Id == id).Select(m => m.ManagerId).Distinct().Count() > 1)
				{
					IsValid = false;
					ValidationErrors.Add(new Exception($"Employee {id} has more than one manager"));
				}
			}
		}
		private void CheckCyclicReference()
		{
			foreach (var employee in _employees.Where(e => e.ManagerId != string.Empty && e.ManagerId != null))
			{
				var manager = _employees.Where(e => e.ManagerId != string.Empty && e.ManagerId != null)
					.FirstOrDefault(e => e.Id == employee.ManagerId);
				if (manager != null)
					if (manager.ManagerId == employee.Id)
					{
						IsValid = false;
						ValidationErrors.Add(new Exception("Cyclic Reference detected"));
					}
			}
		}
		public long GetManagersBudget(string managerId)
		{
			if (string.IsNullOrWhiteSpace(managerId)) throw new ArgumentNullException(nameof(managerId));
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
