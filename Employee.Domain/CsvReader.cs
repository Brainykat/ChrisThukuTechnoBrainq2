
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Domain
{
	public class CsvReader : ICsvReader
	{

		public async Task<List<Employee>> GetEmployees(string csvFilePath)
		{
			return await Task.Run(() =>
			{
				return File.ReadAllLines(csvFilePath).Skip(1).Where(s => s.Length > 1)
					.Select(l => l.ReadEmployeeFromCSVLine()).ToList();
			});
		}
	}
}
