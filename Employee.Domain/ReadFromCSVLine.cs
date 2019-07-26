using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Domain
{
	public static class ReadFromCSVLine
	{
		static public Employee ReadEmployeeFromCSVLine(this string csvLine)
		{
			string[] parts = csvLine.Split(',');
			string Id = parts[0];
			string ManagerId = parts[1];
			string Salary = parts[2];
			
			decimal.TryParse(Salary, out decimal salary);

			return Employee.Create(Id, ManagerId, salary);
		}
	}
}
