using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Employee.Domain
{
	internal class CsvReader : ICsvReader
	{

		public List<Employee> GetEmployees(string csvFilePath)
		{
			List<Employee> employees = new List<Employee>();
			using (StreamReader sr = new StreamReader(csvFilePath))
			{
				sr.ReadLine(); //Bug
				string csvLine;
				while ((csvLine = sr.ReadLine()) != null)
				{
					//employees.Add(ReadFromCSVLine.ReadEmployeeFromCSVLine(csvLine));
					employees.Add(csvLine.ReadEmployeeFromCSVLine());
				}
			}
			return employees;
		}
	}
}
