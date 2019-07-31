
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Employee.Domain
{
	public class CsvReader : ICsvReader
	{

		public List<Employee> GetEmployees(string csvFilePath)
		{
			return File.ReadAllLines(csvFilePath).Skip(1).Where(s => s.Length > 1)
				.Select(l => l.ReadEmployeeFromCSVLine()).ToList();
		}
	}
}
