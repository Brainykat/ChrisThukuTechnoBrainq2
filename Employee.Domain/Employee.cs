using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Employee.Domain
{
	public class Employee
	{
		public string Id { get; private set; }
		public string ManagerId { get; private set; }

		public long Salary { get; private set; }
		private Employee() { }

		private Employee(string id, string managerId, long salary)
		{
			if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
			if(salary < 0) throw new ArgumentOutOfRangeException(nameof(salary));
			Id = id.Trim();
			ManagerId = managerId;
			Salary = salary;
		}
		public static Employee Create(string id, string managerId, long salary)
		{
			return new Employee(id, managerId, salary);
		}
	}
}
